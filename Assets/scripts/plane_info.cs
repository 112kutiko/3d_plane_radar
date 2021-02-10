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
    [Header("create by info")]
    public string _by;

    void Start()
    {
        AbstractMap[] tmp = FindObjectsOfType<AbstractMap>();
        _map = tmp[0];
      
    }

    void Update()
    {
        convertor();
    }
    void convertor()
    {
        _locations = new Mapbox.Utils.Vector2d(Lat, Long);
       gameObject.transform.localPosition = _map.GeoToWorldPosition(_locations, true);
        float a=transform.position.y +  Alt;
        transform.position = new Vector3(transform.position.x, a, transform.position.z);
        transform.rotation = Quaternion.Euler(0, -Trak, 0);//-track
        if(Lat==0 && Long == 0)
        {
            transform.position = new Vector3(transform.position.x, -100, transform.position.z);
        }
    }
   
}
