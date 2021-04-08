using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class debug_ss : MonoBehaviour
{
    public static debug_ss dms;
    public ScrollRect myScrollRect;
    public string output = "";
    public List<string> saved_text;
    public string tmp="",tmp2;
    public Text con;
    public InputField searc;
 
    // Start is called before the first frame update
    void Start()
    {
        dms = this;
      
    }

    // Update is called once per frame
    void Update()
    {

        if (tmp != output)
        {
            saved_text.Add(output);
            output = string.Empty;
            tmp2 = string.Empty;
            for (int u = 0; u < saved_text.Count; u++)
            {
                tmp2 += saved_text[u];
            }
            con.text = tmp2;
            tmp = output;
        }
        else
        {
            tmp = output;
        }
        //if(saved_text.Count%4 ==0)
        //     myScrollRect.verticalNormalizedPosition = 0.98f;
        //  }
    }

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            output += logString + "\n";

        }

        public void debug_send(string y)
        {

            Debug.Log(y);

        }
        public void check_command()
        {
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
                default:
                    Debug.Log("not found");
                    break;
            }
            searc.text = " ";

        }


    }