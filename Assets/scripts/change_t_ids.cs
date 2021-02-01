using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_t_ids : MonoBehaviour
{
    public GameObject tt;

    // Start is called before the first frame update
    void Start()
    {
            
        GetComponent<TextMesh>().text = "Registracija: " + tt.GetComponent<plane_info>().Reg + "\n ICAO: " + tt.GetComponent<plane_info>().Icao;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
