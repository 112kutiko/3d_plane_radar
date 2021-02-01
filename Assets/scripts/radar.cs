using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

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
	public bool ipy = false; // main off/on
	public Camera main_cam;
	[Header("-----plane cam-----")]
	public List<Camera> plane_cams;
	[Header("----------------")]
    private jsonDataclass jsnData;
	private Coroutine s_host = null;
	private string tmp_ac;
	private int tmp_1=0;//-1
	int tmpi = 0;
	bool first_time_b = false;

	[Header("privati info")]
	[SerializeField] private Vector3 spawn_position;
    [SerializeField] private int lektuvu_zonoje;
	[Header("lektuvai")]
	public List<IdList> pl_List, tempory_plane,junk_p;

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
		tmp_ac = "";
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
				Debug.Log(jsnData.acList.Count);

               if (first_time_b == false)
               {
			    first_time();
				first_time_b = !first_time_b;
                }
                else
                {
				data_update();
				check_or_exsist();
				}

				
            }
        }
        else{ list_ac.text = ""; }
	}

	IEnumerator update_plane()
	{
		yield return new WaitForSeconds(5f);
		Debug.Log("update");
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
		if (tmp_1 != y)
		{
			main_cam.enabled = false;
			main_cam_gb.SetActive(ipy);
			ipy = !ipy;
			Debug.Log("cam  id: " +y+ " act: "+ipy);

			pl_List[y].plane.GetComponent<plane_cam_hold>().cam_play();
            if (tmp_1 != -1)
            {
				pl_List[tmp_1].plane.GetComponent<plane_cam_hold>().cam_play();
			}
			tmp_1 = y;

		}
	}

	public void first_time()
    {
		List<string> options = new List<string>();
		junk_p = tempory_plane;
		if (pl_List.Count == 0)
		{
			Debug.Log("start first data create");
			Debug.Log("create plane data " + pl_List.Count);

			foreach (IdList x in tempory_plane)
			{

				Debug.Log("plane id: " + x.Id + "reg: " + x.Reg + "plane: " + x);
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
				if (tmp_ac == "")
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
			select_box_down_update(options);
			text_box_update(tmp_ac);

			pl_List = tempory_plane;
			Debug.Log("end data update");


		}
        else
        {
			Debug.Log("no plane in radar");
		}
	}

	public void data_update()
    {
		List<string> options = new List<string>();
		m_Dropdown.ClearOptions();
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
					tempory_plane.Remove(tempory_plane[a]);
					break;
				}
			}
		}
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
			
			}
		}	select_box_down_update(options);
				text_box_update(tmp_ac);
	}

	public void check_or_exsist()
    {
		Debug.Log("dell old data");
		List<string> options = new List<string>();
		m_Dropdown.ClearOptions(); 
		for (int u=0; u < pl_List.Count; u++)
            { 
				if (!tempory_plane.Contains(pl_List[u]))
                {

					for (int i = 0; i< options.Count; i++)
					{
						if (options[i] == " Icao " + pl_List[u].Icao) { options.RemoveAt(i);}
					}

					m_Dropdown.AddOptions(options);
					Destroy(pl_List[u].plane);
					pl_List.Remove(pl_List[u]);
					
					tmp_ac = "";
					for (int y=0;y< pl_List.Count; y++)
                    {
						if (tmp_ac == "")
						{
							tmp_ac = " Icao " + pl_List[y].Icao + " call " + pl_List[y].Call + " \n";
						}
						else
						{
							tmp_ac = tmp_ac + " Icao " + pl_List[y].Icao + " call " + pl_List[y].Call + " \n";
						}
						
					}
				select_box_down_update(options);
				text_box_update(tmp_ac);
			}
				}
		}

	public void text_box_update(string a)
	{
		list_ac.text = "";
		list_ac.text = a;
	}

	public void select_box_down_update(List<string> a)
    {
		m_Dropdown.ClearOptions();
		m_Dropdown.AddOptions(a);
	}


	}