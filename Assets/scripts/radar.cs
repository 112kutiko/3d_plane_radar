using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;

public class radar : MonoBehaviour
{

	#region kintamieji
	public static radar  instance;
	[Header("vieða info")]
	public string jsonUrl="http://127.0.0.1/VirtualRadar/AircraftList.json";
	public List<GameObject> plane_pl;
	public GameObject front_plane_go;
	public GameObject  main_cam_gb;
	public GameObject fog;
	public Transform parent;
	public Text list_ac;
	[Header("map info")]
	[SerializeField]
	AbstractMap _map;
	[SerializeField]
	AbstractMap main_script;
	public int max_view_all=900,max_plane_view=800;
	public int min_view_all = 0, min_plane_view=0;
	[Header("-----change cam-----")]
	public int now_use_cam_id=-1;
	public bool ipy = true; // main off/on
	public Camera main_cam;
	public InputField searc_plane;
	public string _now_plane="";
	[Header("------------------")]
    private jsonDataclass jsnData;
	private Coroutine s_host = null;
	private string tmp_ac= string.Empty;
	int tmpi = -1;
	public bool first_time_b = false; 
	[Header("privati info")]
	[SerializeField] private Vector3 spawn_position;
    [SerializeField] private int lektuvu_zonoje;
	[Header("lektuvai")]
	public List<IdList> pl_List, tempory_plane;
	public  string api_img_link = "https://www.airport-data.com/api/ac_thumb.json?";
	public string api_img_back = "&n=N";
	#endregion

	void Start()
    {
		instance = this;
		AbstractMap[] tmp = FindObjectsOfType<AbstractMap>();
		_map = tmp[0];
		main_script = _map.GetComponent<AbstractMap>();
		if (PlayerPrefs.HasKey("url_1")){jsonUrl=PlayerPrefs.GetString("url_1");}
		else{jsonUrl = "http://127.0.0.1/VirtualRadar/AircraftList.json";}
		 StartCoroutine(getDate());
	//	Debug.Log("is: " + main_script.CurrentExtent);
	}
    void Update()
    {
		if (s_host == null)
		{ s_host = StartCoroutine(update_plane()); }
	}
	IEnumerator getDate()
    {
 
		WWW _www = new WWW(jsonUrl);
		yield return _www;
		if (_www.error == null) 
        {
			ProcessJsonDate(_www.text);
        }else
		{
			Debug.Log("some error");
			Debug.Log(_www.error);
		}
  
	}
	private void ProcessJsonDate(string _url){
 
		jsonDataclass jsnData = JsonUtility.FromJson<jsonDataclass>(_url);

		lektuvu_zonoje = jsnData.acList.Count;
		if(jsnData.acList.Count!=0)
        {
			tempory_plane=jsnData.acList;

			Debug.Log("____________________________");
			debug_ss.dms.debug_send("ðaltinis: " + jsnData.src + " lektuvu zonoje: " + jsnData.acList.Count);
			debug_ss.dms.debug_send("____________________________");
				if (first_time_b == false)
               { 
					first_time(jsnData.acList);
			     	first_time_b = true;
					tmpi=pl_List.Count;
				}
                else
				{ 
					data_update();
					tempory_plane = jsnData.acList;
					tmpi = pl_List.Count;
				}
                 if (pl_List.Count != 0)
                    {
					tempory_plane = jsnData.acList;
					check_or_exsist();
					tmpi = pl_List.Count;
				}

        }else{
			dell_all();
		
		}
	}
	IEnumerator update_plane()
	{
		yield return new WaitForSeconds(5f); 
		StartCoroutine(getDate());
		s_host = null;
	}

    #region cameros
    public void main_cam_activator()
    {
		fog.SetActive(true);
		main_script.SetExtentOptions(now_cam_by(main_cam_gb, min_view_all, max_view_all));
		main_cam.enabled = true;
		debug_ss.dms.debug_send("main cam: " + main_cam.enabled);
		ipy = true;
			for (int i = 0; i < pl_List.Count; i++)
            {
                if (pl_List[i].Icao==_now_plane)
                {
				debug_ss.dms.debug_send("off cam");
					pl_List[i].plane.GetComponent<plane_cam_hold>().plane_cam.enabled = false;
					_now_plane = "";
					now_use_cam_id = -1;

				 
				break;
                }
		
        }
		now_use_cam_id = -1;
	}
	public void change_cam()
    { 
		string _id = searc_plane.text;
	 

		if (pl_List.Count == 0)
        {
			debug_ss.dms.debug_send("bug plane:" +_id+ "now "+now_use_cam_id);

		}
        else
        {
		for(int c = 0; c < pl_List.Count; c++)
				{
					if (pl_List[c].Icao == _id)
					{fog.SetActive(false);
					if (main_cam.enabled == false)
						{
						ipy = false;
							for (int s = 0; s < pl_List.Count; s++)
							{
								if (pl_List[s].Icao == _id)
								{


									if (now_use_cam_id != -1)
									{
										pl_List[now_use_cam_id].plane.GetComponent<plane_cam_hold>().cam_play();
									}
									now_use_cam_id = s;
									pl_List[now_use_cam_id].plane.GetComponent<plane_cam_hold>().cam_play();
									main_script.SetExtentOptions(now_cam_by(pl_List[now_use_cam_id].front_show, min_plane_view, max_plane_view));
								debug_ss.dms.debug_send("cam  id: " + now_use_cam_id + "main cam: " + ipy);
									break;
								}
							}

						}
						else
						{
							main_cam.enabled = false;
							ipy = false;
							for (int s = 0; s < pl_List.Count; s++)
							{
								if (pl_List[s].Icao == _id)
								{
									if (now_use_cam_id != -1)
									{
										pl_List[now_use_cam_id].plane.GetComponent<plane_cam_hold>().cam_play();
									}
									now_use_cam_id = s;
									pl_List[now_use_cam_id].plane.GetComponent<plane_cam_hold>().cam_play();
								main_script.SetExtentOptions(now_cam_by(pl_List[now_use_cam_id].front_show, min_plane_view, max_plane_view));
								debug_ss.dms.debug_send("cam  id: " + now_use_cam_id + "main cam: " + ipy+" w ");
								}
							}  
						}
						break;
					}
					if (c== pl_List.Count) { debug_ss.dms.debug_send("cam  not exsit ");  }
				}
        }
		

	}

#endregion


	public void first_time(List<IdList> a)
    {

	 

		foreach (IdList x in a)
			{
				plane_spawner(x, "first time");
			}	
	}
	public void data_update()
    { 
		if (tempory_plane.Count != 0)
		{
			foreach (IdList x in tempory_plane)
			{
				if (GameObject.Find(x.Icao) != null)
				{
					plane_update(x);
				}
                else
                {
					plane_spawner(x, "secon time");
				}
			}
		}
	}
	public void check_or_exsist()
    {
		List<string> _on = new List<string>(),_off = new List<string>();
	
		for (int u = 0; u < tempory_plane.Count; u++)
		{
			for (int a = 0; a < pl_List.Count; a++)
			{
				if (tempory_plane[u].Id == pl_List[a].Id)
				{
					_on.Add(pl_List[a].Id);
				}
			}
		}
		for (int a = 0; a < pl_List.Count; a++)
		{
			_off.Add(pl_List[a].Id);
		}
		List<string> _d = _off.Except(_on).ToList();

			for (int s = 0; s < pl_List.Count; s++)
			{
				for(int y = 0; y < _d.Count; y++)
            {
                if (pl_List[s].Id == _d[y])
                {
					dell_checked_plane(pl_List, pl_List[s]);
				}
            }
	
			
			}		
		tempory_plane.Clear();
		}
	public void text_box_update(string a)
	{
		list_ac.text = "";
		list_ac.text = a;
		tmp_ac = "";
	}
	public void dell_checked_plane(List<IdList> s, IdList a)
    {
        if (_now_plane==a.Icao)
        {
			a.plane.GetComponent<plane_cam_hold>().cam_back();

		}
		Destroy(a.front_show);
		Destroy(a.plane);
		s.Remove(a);
		text_reload();
	}
	public void plane_spawner(IdList a,string then)
	{
	 

		spawn_position.z = a.Long;
			spawn_position.x = a.Lat;
			spawn_position.y = (int)(a.Alt * 0.0003048f); 
		    GameObject plo = this_plane(a.Mil);
			GameObject se = Instantiate(plo, spawn_position, Quaternion.identity, parent);
			plane_info plane_tmp = se.GetComponent<plane_info>();
			plane_tmp.Id = a.Id;
			plane_tmp.Reg = a.Reg;
			plane_tmp.Icao = a.Icao;
			plane_tmp.Call = a.Call;
			plane_tmp.Type = a.Type;
			plane_tmp.Mdl = a.Mdl;
			plane_tmp.From = a.From;
			plane_tmp.To = a.To;
			plane_tmp.Op = a.Op;
			plane_tmp.Alt = (int)spawn_position.y;
			plane_tmp.Spd = a.Spd;
			plane_tmp.Lat = a.Lat;
			plane_tmp.Long = a.Long;
			plane_tmp.Trak = a.Trak;
			plane_tmp.Mil = a.Mil;
			se.name = a.Icao;
			plane_tmp.front_show = plane_tmp.frGo();
			plane_tmp._by = then;
			a.plane = se;
		    a.front_show = plane_tmp.front_show;
			pl_List.Add(a);
			text_reload();
	}
	public void plane_update(IdList a)
    {
		GameObject _tmp = GameObject.Find(a.Icao);
	    plane_info p_tmp = _tmp.GetComponent<plane_info>();
		p_tmp.Long = a.Long;
		p_tmp.Lat = a.Lat;
		p_tmp.Alt = (int)(a.Alt * 0.0003048f);
	    p_tmp.Id = a.Id;
		p_tmp.Reg = a.Reg;
		p_tmp.Call = a.Call;
		p_tmp.Type = a.Type;
		p_tmp.Mdl = a.Mdl;
		p_tmp.From = a.From;
		p_tmp.To = a.To;
		p_tmp.Op = a.Op; 
		p_tmp.Spd = a.Spd;
		p_tmp.Trak = a.Trak;
		text_reload();
		
	}
	public void text_reload()
    {
		tmp_ac = "";
        if (pl_List.Count != 0)
        {
		for (int i= 0;i<pl_List.Count;i++)
        {
			if (tmp_ac =="")
			{
				tmp_ac = " Icao " + pl_List[i].Icao + " call " + pl_List[i].Call + " \n";
			}
			else
			{
				tmp_ac = tmp_ac + " Icao " + pl_List[i].Icao + " call " + pl_List[i].Call + " \n";
			}
		}
	
        }	text_box_update(tmp_ac);
	}
	public GameObject this_plane(string i)
    {
		GameObject o= plane_pl[0];
        if (i == "true")
		{
			 
			o = plane_pl[1];
        }
        else
        {
			 
		}
		return o;
    }
	public ExtentOptions now_cam_by(GameObject zip_is_cam,int i= 0, int u= 2000) {

		RangeAroundTransformTileProviderOptions zipo = new RangeAroundTransformTileProviderOptions();
		zipo.visibleBuffer =i;
		zipo.disposeBuffer = u;

		zipo.targetTransform = zip_is_cam.transform;



		return zipo;
	}

	public void dell_all() {
        if (pl_List.Count != 0)
        {
		for(int o=0;o< pl_List.Count; o++)
			{
				if(pl_List[o].Icao== _now_plane)
                {
				pl_List[o].plane.GetComponent<plane_cam_hold>().cam_back();
                }
				Destroy(pl_List[o].front_show);
				Destroy(pl_List[o].plane);
                if (tempory_plane.Count != 0) {
					if (pl_List[o].Icao == _now_plane)
					{
						pl_List[o].plane.GetComponent<plane_cam_hold>().cam_back();
					}
					Destroy(tempory_plane[o].plane);
				}
				}
				pl_List.Clear();
			tempory_plane.Clear();

		}
		text_reload();
	}
}