using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class debug_ss : MonoBehaviour
{
    public static debug_ss dms;

    public string output = "";
    public string hh,tmp="";
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
        hh +=output;
        output = string.Empty;
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
        output += logString + "\n";
   
    }

    public void debug_send(string y)
    {
     
        Debug.Log(y);
 
    }
}
