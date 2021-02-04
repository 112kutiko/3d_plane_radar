using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class radar : MonoBehaviour
{
	public static radar  instance;
	
	[Header("vieða info")]
	public string jsonUrl="http://127.0.0.1/VirtualRadar/AircraftList.json";
	public GameObject plane_pl,main_cam_gb;
	public Transform parent;
	public Text list_ac;
	[Header("-----change cam-----")]
	public int now_use_cam_id=-1;
	public bool ipy = false; // main off/on
	public Camera main_cam;
	public InputField searc_plane;
	[Header("------------------")]
    private jsonDataclass jsnData;
	private Coroutine s_host = null;
	private string tmp_ac= string.Empty;
	int tmpi = -1;
	public bool first_time_b = false;
	private List<int> dell_nr = new List<int>();
	[Header("privati info")]
	[SerializeField] private Vector3 spawn_position;
    [SerializeField] private int lektuvu_zonoje;
	[Header("lektuvai")]
	public List<IdList> pl_List, tempory_plane, third_plane;

	void Start()
    {
		instance = this;
		if (PlayerPrefs.HasKey("url_1")){jsonUrl=PlayerPrefs.GetString("url_1");}
		else{jsonUrl = "http://127.0.0.1/VirtualRadar/AircraftList.json";}
		 StartCoroutine(getDate());
    }

    void Update()
    {
		if (s_host == null)
		{ s_host = StartCoroutine(update_plane()); }
	}

	IEnumerator getDate()
    {
		Debug.Log("_______________");
		Debug.Log("start Data gain");
		WWW _www = new WWW(jsonUrl);
		yield return _www;
		if (_www.error == null) 
        {
			ProcessJsonDate(_www.text);
        }else
		{
			Debug.Log("some error");
		}
		Debug.Log("stop Data gain");
		Debug.Log("_______________");
	}

	private void ProcessJsonDate(string _url){
		bool fg = false;
		jsonDataclass jsnData = JsonUtility.FromJson<jsonDataclass>(_url);

		lektuvu_zonoje = jsnData.acList.Count;
		if(jsnData.acList.Count!=0 && jsnData.acList.Count != null)
        {
			tempory_plane=jsnData.acList;
            if (tempory_plane.Count == 0)
            {
				Debug.Log("data get fail");
			}
            else
            {
				Debug.Log("____________________________");
				Debug.Log("ðaltinis: " + jsnData.src + " lektuvu zonoje: " + jsnData.acList.Count);
				Debug.Log("____________________________");
				if (first_time_b == false)
               {
					Debug.Log("first time");
					first_time();
				first_time_b = !first_time_b;
                }
                else
                {
                    if (tempory_plane.Count != 0)
                    {
					data_update();
					tempory_plane = jsnData.acList;
                    }
                    if (pl_List.Count != 0)
                    {
					check_or_exsist();
                    }

				}

				
            }
        }
        else{ list_ac.text = ""; }
	}

	IEnumerator update_plane()
	{
		yield return new WaitForSeconds(5f); 
		StartCoroutine(getDate());
		s_host = null;
	}

	public void main_cam_activator(bool isu=true)
    {
		main_cam.enabled = isu;
        if (pl_List.Count != 0)
        {for(int i = 0; i < pl_List.Count; i++)
		{
			pl_List[i].plane.GetComponent<plane_cam_hold>().cam_play();

		}
        }
		
	}
	public void change_cam(int y)
    {
		if(main_cam.enabled == false) {
			pl_List[now_use_cam_id].plane.GetComponent<plane_cam_hold>().cam_play();
			y = now_use_cam_id;
			pl_List[y].plane.GetComponent<plane_cam_hold>().cam_play();
		} else {
		if (tmpi != -1)
		{
		  main_cam.enabled = ipy;
		  Debug.Log("cam  id: " + y + " act: " + ipy);
		  ipy = !ipy; 
		  pl_List[y].plane.GetComponent<plane_cam_hold>().cam_play();
          now_use_cam_id = y;
		}
		
		}
		
	}

	public void first_time()
    {
		
			Debug.Log("start first data create");
			foreach (IdList x in tempory_plane)
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
		if (pl_List.Count != 0)
		{ 
			text_box_update(tmp_ac);
		}
	}

	public void check_or_exsist()
    {
		Debug.Log("dell old data");
		 

		for (int u = 0; u < tempory_plane.Count; u++)
		{
			for (int a = 0; a < pl_List.Count; a++)
			{
				if (tempory_plane[u].Id == pl_List[a].Id)
				{
					third_plane.Add(pl_List[a]);
				}
			}
		}
		for (int a = 0; a < pl_List.Count; a++)
		{
            if (third_plane.Contains(pl_List[a]) == false) { Destroy(pl_List[a].plane);pl_List.Remove(pl_List[a]);}
		}
		for (int y=0;y< pl_List.Count; y++)
                    {
						if (tmp_ac == string.Empty)
						{
							tmp_ac = " Icao " + pl_List[y].Icao + " call " + pl_List[y].Call + " \n";
						}
						else
						{
							tmp_ac = tmp_ac + " Icao " + pl_List[y].Icao + " call " + pl_List[y].Call + " \n";
						}
						
					}
				if (pl_List.Count != 0)
				{
					text_box_update(tmp_ac);
				}
				tempory_plane.Clear();
		}

	public void text_box_update(string a)
	{
		list_ac.text = a;
	tmp_ac = string.Empty;
	}


	public void dell_checked_plane(List<IdList> a)
    {
		if (dell_nr.Count != 0)
		{
			if (a.Count != 0)
			{
				Debug.Log("plane skaièius: " + a.Count);
				Debug.Log("trinamu skaièius: " + dell_nr.Count);
				int tmp_c = dell_nr.Count;
				for (int i = 0; i < tmp_c; i++)
				{
					Destroy(a[dell_nr[i]].plane);
					a.Remove(a[dell_nr[i]]);
				}

			}
			dell_nr = new List<int>();
			Debug.Log("dell skaièius: " + dell_nr.Count);
		}

    }
	public void plane_spawner(IdList a,string then)
	{
		
			spawn_position.z = a.Long;
			spawn_position.x = a.Lat;
			spawn_position.y = (a.Alt * 0.0003048f);
			GameObject se = Instantiate(plane_pl, spawn_position, Quaternion.identity, parent);
			se.GetComponent<plane_info>().Id = a.Id;
			se.GetComponent<plane_info>().Reg = a.Reg;
			se.GetComponent<plane_info>().Icao = a.Icao;
			se.GetComponent<plane_info>().Call = a.Call;
			se.GetComponent<plane_info>().Type = a.Type;
			se.GetComponent<plane_info>().Mdl = a.Mdl;
			se.GetComponent<plane_info>().From = a.From;
			se.GetComponent<plane_info>().To = a.To;
			se.GetComponent<plane_info>().Op = a.Op;
			se.GetComponent<plane_info>().Alt = (int)(a.Alt * 0.0003048f);
			se.GetComponent<plane_info>().Spd = a.Spd;
			se.GetComponent<plane_info>().Lat = a.Lat;
			se.GetComponent<plane_info>().Long = a.Long;
			se.GetComponent<plane_info>().Trak = a.Trak;
			se.name = a.Icao;
			se.GetComponent<plane_info>()._by = then;
			a.plane = se;

			if (tmp_ac == string.Empty)
			{
				tmp_ac = " Icao " + a.Icao + " call " + a.Call + " \n";
			}
			else
			{
				tmp_ac = tmp_ac + " Icao " + a.Icao + " call " + a.Call + " \n";
			}
		tmpi++;
		pl_List = tempory_plane;
		if (pl_List.Count != 0)
		{
			text_box_update(tmp_ac);
		}
		tempory_plane.Clear(); 
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
	}



	}