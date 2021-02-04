using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            string t1 = _lat.text.ToString();
            string t2 = _long.text.ToString();
            _c_lat =  float.Parse(t1);
            _c_long = float.Parse(t2);

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
