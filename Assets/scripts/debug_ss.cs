using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class debug_ss : MonoBehaviour
{
    public static debug_ss dms;

    public string output = "";
    public string stack = "";
    string hh,tmp="";
    public Text con;
    // Start is called before the first frame update
    void Start()
    {
        dms = this;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (tmp!= output)
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
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
    }

    public void debug_send(string y)
    {
     
        Debug.Log(y);
 
    }
}
