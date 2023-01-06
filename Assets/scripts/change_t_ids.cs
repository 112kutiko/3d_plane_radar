using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_t_ids : MonoBehaviour
{
    public GameObject tt;
    void Start() {     GetComponent<TextMesh>().text = "Reg: " + tt.GetComponent<plane_info>().Reg + "\n ICAO: " + tt.GetComponent<plane_info>().Icao;}
}
