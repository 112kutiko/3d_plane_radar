using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plane_info_ui : MonoBehaviour
{
    public static plane_info_ui instance;
    public GameObject panel;
    public Text plane_id, plane_reg,plane_Icao,plane_call,plane_type,plane_mdl,plane_from,plane_to,plane_op,plane_alt,plane_spd,plane_lat,plane_long,plane_track;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
