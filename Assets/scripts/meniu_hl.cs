using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;


public class meniu_hl : MonoBehaviour
{
    public Text Username_field, _long, _lat;
    public string url_link;
    public float _c_long=0f, _c_lat = 0f;

    private int cop = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
        if (cop == 0)
        {
            url_link = Username_field.text.ToString();
            PlayerPrefs.SetString("url_1", url_link);  
            float tmp_c_m;
                    if (float.TryParse(_lat.text, out tmp_c_m))
                    {
                        Debug.Log("lat: "+tmp_c_m);
                        _c_lat = tmp_c_m;
                    }
                    else
                    {
                        Debug.Log("lat fail: "+ _lat.text);
                    }
                      if (float.TryParse(_long.text, out _c_long))
                    {
                        Debug.Log("long: " + _c_long);
                    }
                    else
                     {
                        Debug.Log("long fail: " + _long.text);

                     }
             PlayerPrefs.SetFloat("_lat_c",_c_lat);
             PlayerPrefs.SetFloat("_long_c", _c_long); 
        }
        

    }

    public void map_on()
    {
         if (url_link.Contains("AircraftList.json"))
        {
            if(_c_lat==0f && _c_long == 0f)
            {
             Debug.Log("1");
             SceneManager.LoadScene(1);
             cop = 1;
            }
            else
            {
                Debug.Log("fail");
            }
           
        }
        else
        {
            if (_c_lat == 0f && _c_long == 0f)
            {
                cop = 1;
                Debug.Log("2");
                url_link = "http://127.0.0.1/VirtualRadar/AircraftList.json";
                PlayerPrefs.SetString("url_1", url_link);
                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("fail");
            }
        }



    }
}
