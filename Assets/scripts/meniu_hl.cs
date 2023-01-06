using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class meniu_hl : MonoBehaviour{
    public Text Username_field, _long, _lat;
    public string url_link;
    public float _c_long=0f, _c_lat = 0f;
    public GameObject m_ui, s_ui;

 void Update() {
  ParseLatitude(_lat.text);
  ParseLongitude(_long.text);
                }

    public void map_on() {
         if (url_link.Contains("AircraftList.json"))
        {
            if(_c_lat!=0f && _c_long != 0f)
            {
             Debug.Log("1");
             SceneManager.LoadScene(1);
    }else{
                Debug.Log("fail");
            }
         } else {
            if (_c_lat != 0f && _c_long != 0f)
            {
                Debug.Log("2");
                url_link = "http://127.0.0.1/VirtualRadar/AircraftList.json";
                PlayerPrefs.SetString("url_1", url_link);
                SceneManager.LoadScene(1);
            }  else {
                Debug.Log("fail");
            }
        }
    }
    public void ParseLatitude(string lat)  {
        float tmp_c_m;
        if (float.TryParse(lat, out tmp_c_m))
        {
            Debug.Log("lat: " + tmp_c_m);
            _c_lat = tmp_c_m;

            PlayerPrefs.SetFloat("_lat_c", _c_lat);
        
        }else {
            Debug.Log("lat fail: " + lat);
        }

    }

    public void ParseLongitude(string lon)  {
        float tmp_c_m;
        if (float.TryParse(lon, out  tmp_c_m))
        {
            _c_long=tmp_c_m;
            Debug.Log("long: " + _c_long);
            PlayerPrefs.SetFloat("_long_c", _c_long);
        }else{
            Debug.Log("long fail: " + lon);
        }
    }
}
