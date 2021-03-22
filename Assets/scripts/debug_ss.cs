using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class debug_ss : MonoBehaviour
{
    public string output = "";
    public string stack = "";
    string hh,tmp="";
    public Text con;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
      if(tmp!= output)
        {
        hh +=output + "\n";
        con.text = hh;  
        tmp = output;
        }
        else
        {
            tmp = output;
        }

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
        output = logString;
        stack = stackTrace;
    }
}
