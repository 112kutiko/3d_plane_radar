using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;


public class plane_info : MonoBehaviour
{
    [Header("map info")]
    [SerializeField]
    AbstractMap _map;
    [SerializeField]
    [Geocode]
    string _locationStrings;
    Mapbox.Utils.Vector2d _locations;

    [Header("plane info")]
    public string Id;
    public string Reg;
    public string Icao;
    public string Call;
    public string Type;//typas
    public string Mdl; //detalus typas
    public string From; //ið
    public string To; //i
    public string Op;//operatorius
    public int Alt; //km
    public float Spd; //km/h
    public float Lat; //cor
    public float Long;//cor
    public float Trak; //direction
    public string api_img_mid;
    public string Mil; //military
    bool first = false;
    public GameObject front_show;
    [Header("plane img info")]
    public string ats;
    public List<img_list> img_ups;// nuotrauku kiekis
    public string link_img;


    [Header("create by info")]
    public string _by;

    private img_data jsnData;

    void Start()
    {
        AbstractMap[] tmp = FindObjectsOfType<AbstractMap>();
        _map = tmp[0];
        api_img_mid = Icao;
    }

    void Update()
    {
        convertor();
        if (first == false && Icao != "")
        {
            img_get();
            first = true;
            img_picker(img_ups.Count);
        }
        front_sh();
    }
    void convertor()
    {
        api_img_mid = Icao;
        _locations = new Mapbox.Utils.Vector2d(Lat, Long);
        gameObject.transform.localPosition = _map.GeoToWorldPosition(_locations, true);
        float a = transform.position.y + Alt;
        transform.position = new Vector3(transform.position.x, a, transform.position.z);
        transform.rotation = Quaternion.Euler(0, Trak+180, 0);// comp_end(Trak)
        if (Lat == 0 && Long == 0)
        {
            transform.position = new Vector3(transform.position.x, -100, transform.position.z);
        }
    }
    void img_get()
    {
        string full_link = radar.instance.api_img_link + "m=" + api_img_mid + radar.instance.api_img_back;
        Debug.Log(full_link);
        StartCoroutine(getDate(full_link));
    }
    IEnumerator getDate(string u)
    {
        Debug.Log("start img gain");
        WWW _www = new WWW(u);
        yield return _www;
        if (_www.error == null)
        {
            ProcessJsonDate(_www.text);
        }
        else
        {
            Debug.Log("some img error");
        }
        Debug.Log("stop img gain");
    }
    private void ProcessJsonDate(string _url)
    {
        img_data jsnData = JsonUtility.FromJson<img_data>(_url);
        if (jsnData.data != null)
        {
            if (jsnData.status == "200")
            {
     
               link_img = jsnData.data[0].image;
                img_ups = jsnData.data;
                ats = jsnData.status;
            }
            else
            {
                ats = jsnData.status;
            }

        }
        else {   
        Debug.Log("status: " + jsnData.status + " img get fail");
            ats = jsnData.status;
        }
    }
    void img_picker(int zero)
    {
        if (zero > 1)
        {
            int a = Random.Range(0, zero);
            link_img = img_ups[a].image;
        }
    }

    void front_sh() {
        GameObject fpg = radar.instance.front_plane_go;
        if (front_show == null)
        {
            front_show = Instantiate(fpg,gameObject.transform.position, Quaternion.Euler(0, Trak + 180, 0));
            front_show.transform.position += transform.forward * 50;
            front_show.name = "front_" + Icao;
        }
        else
        {
            front_show.transform.rotation = Quaternion.Euler(0, Trak + 180, 0);
            front_show.transform.position = gameObject.transform.position+transform.forward * 50;
            front_show.name = "front_" + Icao;
        }

    }


}