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
	public Dropdown m_Dropdown;
	[Header("-----main cam-----")]
	public int now_use_cam_id=-1;
	public bool ipy = false; // main off/on
	public Camera main_cam;
	[Header("------------------")]
    private jsonDataclass jsnData;
	private Coroutine s_host = null;
	private string tmp_ac= string.Empty;
	int tmpi = -1;
	bool first_time_b = false;
	private List<string> options = new List<string>();
	private List<int> dell_nr = new List<int>();
	[Header("privati info")]
	[SerializeField] private Vector3 spawn_position;
    [SerializeField] private int lektuvu_zonoje;
	[Header("lektuvai")]
	public List<IdList> pl_List, tempory_plane,junk_p, third_plane;

	void Start()
    {
		instance = this;
		if (PlayerPrefs.HasKey("url_1")){jsonUrl=PlayerPrefs.GetString("url_1");}
		else{jsonUrl = "http://127.0.0.1/VirtualRadar/AircraftList.json";}

		Debug.Log("using url: "+ jsonUrl);

		StartCoroutine(getDate());
    }

    void Update()
    {
		if (s_host == null)
		{ s_host = StartCoroutine(update_plane()); }
	}

	IEnumerator getDate()
    {
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
	}

	private void ProcessJsonDate(string _url){
		bool fg = false;
		jsonDataclass jsnData = JsonUtility.FromJson<jsonDataclass>(_url);
		Debug.Log("ðaltinis: " + jsnData.src +" lektuvu zonoje: "+ jsnData.acList.Count);
		lektuvu_zonoje = jsnData.acList.Count;
		if(jsnData.acList.Count!=0 && jsnData.acList.Count != null)
        {
			tempory_plane=jsnData.acList;
            if (jsnData.acList.Count == 0)
            {
				Debug.Log("data get fail");
			}
            else
            {
				Debug.Log("load"); 

               if (first_time_b == false)
               {
			    first_time();
				first_time_b = !first_time_b;
                }
                else
                { 
				data_update();
				tempory_plane = jsnData.acList;
				check_or_exsist();
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
		junk_p = tempory_plane;
		if (pl_List.Count == 0)
		{
			Debug.Log("start first data create");
			foreach (IdList x in tempory_plane)
			{
				spawn_position.z = x.Long;
				spawn_position.x = x.Lat;
				spawn_position.y = (x.Alt * 0.0003048f);
				GameObject se = Instantiate(plane_pl, spawn_position, Quaternion.identity, parent);
				se.GetComponent<plane_info>().Id = x.Id;
				se.GetComponent<plane_info>().Reg = x.Reg;
				se.GetComponent<plane_info>().Icao = x.Icao;
				se.GetComponent<plane_info>().Call = x.Call;
				se.GetComponent<plane_info>().Type = x.Type;
				se.GetComponent<plane_info>().Mdl = x.Mdl;
				se.GetComponent<plane_info>().From = x.From;
				se.GetComponent<plane_info>().To = x.To;
				se.GetComponent<plane_info>().Op = x.Op;
				se.GetComponent<plane_info>().Alt = (int)(x.Alt * 0.0003048f);
				se.GetComponent<plane_info>().Spd = x.Spd;
				se.GetComponent<plane_info>().Lat = x.Lat;
				se.GetComponent<plane_info>().Long = x.Long;
				se.GetComponent<plane_info>().Trak = x.Trak;
				x.plane = se;

				if (tmp_ac == string.Empty)
				{
					tmp_ac = " Icao " + x.Icao + " call " + x.Call + " \n";
				}
				else
				{
					tmp_ac = tmp_ac + " Icao " + x.Icao + " call " + x.Call + " \n";
				}
				string option = " Icao " + x.Icao;
				options.Add(option);
				tmpi++;
			}
			pl_List = tempory_plane;
            if (pl_List.Count!=0)
			{
			select_box_down_update(options);
			text_box_update(tmp_ac);
            }
			tempory_plane.Clear();
		}
        else
        {
			Debug.Log("no plane in radar");
		}
	}

	public void data_update()
    {
		options.Clear();
		Debug.Log("start data update");
		for (int a = 0; a < tempory_plane.Count; a++)
		{
			for (int u = 0; u < pl_List.Count; u++)
			{
				if (pl_List[u].Id == tempory_plane[a].Id)
				{
					pl_List[u].Reg = tempory_plane[a].Reg;
					pl_List[u].Icao = tempory_plane[a].Icao;
					pl_List[u].Call = tempory_plane[a].Call;
					pl_List[u].Type = tempory_plane[a].Type;
					pl_List[u].Mdl = tempory_plane[a].Mdl;
					pl_List[u].From = tempory_plane[a].From;
					pl_List[u].To = tempory_plane[a].To;
					pl_List[u].Op = tempory_plane[a].Op;
					pl_List[u].Alt = (int)(tempory_plane[a].Alt * 0.0003048f);
					pl_List[u].Spd = tempory_plane[a].Spd;
					pl_List[u].Lat = tempory_plane[a].Lat;
					pl_List[u].Long = tempory_plane[a].Long;
					pl_List[u].Trak = tempory_plane[a].Trak;
					dell_nr.Add(a);
					break;
				}
			}
		}
		dell_checked_plane(tempory_plane);

		Debug.Log("add new data");
		if (tempory_plane.Count != 0)
		{
			foreach (IdList x in tempory_plane)
			{

				spawn_position.z = x.Long;
				spawn_position.x = x.Lat;
				spawn_position.y = (x.Alt * 0.0003048f);
				GameObject se = Instantiate(plane_pl, spawn_position, Quaternion.identity, parent);
				se.GetComponent<plane_info>().Id = x.Id;
				se.GetComponent<plane_info>().Reg = x.Reg;
				se.GetComponent<plane_info>().Icao = x.Icao;
				se.GetComponent<plane_info>().Call = x.Call;
				se.GetComponent<plane_info>().Type = x.Type;
				se.GetComponent<plane_info>().Mdl = x.Mdl;
				se.GetComponent<plane_info>().From = x.From;
				se.GetComponent<plane_info>().To = x.To;
				se.GetComponent<plane_info>().Op = x.Op;
				se.GetComponent<plane_info>().Alt = (int)(x.Alt * 0.0003048f);
				se.GetComponent<plane_info>().Spd = x.Spd;
				se.GetComponent<plane_info>().Lat = x.Lat;
				se.GetComponent<plane_info>().Long = x.Long;
				se.GetComponent<plane_info>().Trak = x.Trak;
				x.plane = se;
				pl_List.Add(x);
				tmp_ac = tmp_ac + " Icao " + x.Icao + " call " + x.Call + " \n";
				string option = " Icao " + x.Icao;
				tmpi++;
				options.Add(option);
			}
		}
		if (pl_List.Count != 0)
		{
			select_box_down_update(options);
			text_box_update(tmp_ac);
		}
	}

	public void check_or_exsist()
    {
		Debug.Log("dell old data");

		List<string> options_tmp = new List<string>();

		for (int u = 0; u < tempory_plane.Count; u++)
		{
			for (int a = 0; a < pl_List.Count; a++)
			{
				if (tempory_plane[u].Id == pl_List[a].Id)
				{
					third_plane.Add(pl_List[a]);
					string option = " Icao " + pl_List[a].Icao;
					options_tmp.Add(option);
				}
			}
		}
		for (int a = 0; a < pl_List.Count; a++)
		{
            if (third_plane.Contains(pl_List[a]) == false) { Destroy(pl_List[a].plane);pl_List.Remove(pl_List[a]);}
		}
		options = options_tmp;
		select_box_down_update(options);
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
		        options.Clear();
		}

	public void text_box_update(string a)
	{
		list_ac.text = a;
	tmp_ac = string.Empty;
	}

	public void select_box_down_update(List<string> a)
    {
		m_Dropdown.ClearOptions();
		m_Dropdown.AddOptions(a);
	}
	public void dell_checked_plane(List<IdList> a)
    {
		for(int i=0;i<dell_nr.Count;i++)
        {
			a.Remove(a[dell_nr[i]]);
		}
		dell_nr.Clear();
    }


	}