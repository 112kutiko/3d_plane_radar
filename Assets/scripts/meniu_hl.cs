using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class meniu_hl : MonoBehaviour
{
    public Text Username_field;
    public string url_link;
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
        }
    }

    public void map_on()
    {
         if (url_link.Contains("AircraftList.json"))
        {
            Debug.Log("1");
            SceneManager.LoadScene(1);
            cop = 1;
        }
        else
        {
            cop = 1;
            Debug.Log("2");
            url_link = "http://127.0.0.1/VirtualRadar/AircraftList.json";
            PlayerPrefs.SetString("url_1", url_link);
            SceneManager.LoadScene(1); 
        }



    }
   
    //jsonUrl = "http://127.0.0.1/VirtualRadar/AircraftList.json";
}
