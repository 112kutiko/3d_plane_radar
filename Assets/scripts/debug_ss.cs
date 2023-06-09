using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class debug_ss : MonoBehaviour
{
    public static debug_ss dms;
    public ScrollRect myScrollRect;
    public string output = "";
    public List<string> saved_text,tempo;
    public string tmp="",tmp2;
    public Text con;
    public InputField searc;
    public float scrollSpeed =  4f;
    public RectTransform m_ContentRectTransform;
    public GameObject sc;

    void Start()  {dms = this;    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)){
            sc.SetActive(!sc.activeSelf);
        }
        if (sc.activeSelf == true){
            if (tmp != output){
            if ((saved_text.Count % 6) == 0){
                tmp2 = string.Empty;
                debug_send("clear"); }
            saved_text.Add(output);
            tempo.Add(output);
            output = string.Empty;
 
            for (int u = 0; u < tempo.Count; u++) {
                tmp2 += tempo[u];
            }
            tempo.Clear();
            con.text = tmp2;
            tmp = output;
        }else{
            tmp = output;
        }
        }
    }

        void OnEnable() {Application.logMessageReceived += HandleLog;}

        void OnDisable(){ Application.logMessageReceived -= HandleLog;}

        void HandleLog(string logString, string stackTrace, LogType type){ output += logString + "\n"; }
        public void debug_send(string y) {Debug.Log(y); }
        public void check_command()   {
            string txh = searc.text.ToString();
            char sq = ' ';
            string[] words = txh.Split(sq);
            switch (words[0])
            {
                case "test":
                    Debug.Log("test work");
                    break;
                case "json_url":
                    Debug.Log("url: " + words[1]);
                    radar.instance.jsonUrl = words[1];
                    break;
                case "stopWatch":
                     Application.Quit();
                break;
                case "cords":
                float lat = PlayerPrefs.GetFloat("_lat_c");
                float longi = PlayerPrefs.GetFloat("_long_c");
                Debug.Log("cords: " + lat + " " + " "+ longi);

                break;

            default:
                    Debug.Log("not found");
                    break;
            }
            searc.text = " ";

        }
    }