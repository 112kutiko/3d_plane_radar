using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plane_info_ui : MonoBehaviour
{
    public static plane_info_ui instance;
    public GameObject panel;
    public Text plane_id, plane_reg,plane_Icao,plane_call,plane_type,plane_mdl,plane_from,plane_to,plane_op,plane_alt,plane_spd,plane_lat,plane_long,plane_track;
    public bool on_off = false;
    public string _tmp_plane = string.Empty;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(radar.instance._now_plane!=""&& radar.instance._now_plane != string.Empty && radar.instance.now_use_cam_id != -1) { on_off = true; on_info(on_off); _tmp_plane = radar.instance._now_plane; set_info(); } else { on_off = false; on_info(on_off); }
    }
    public void on_info(bool i) { panel.SetActive(i); }

    public void set_info()
    {
        for(int i=0;i< radar.instance.pl_List.Count; i++)
        {
            if(_tmp_plane == radar.instance.pl_List[i].Icao)
            {
                plane_id.text = "id: " + radar.instance.pl_List[i].Id;
                plane_reg.text = "registration: " + radar.instance.pl_List[i].Reg;
                plane_Icao.text = "ICao: " + radar.instance.pl_List[i].Icao;
                plane_call.text = "Call: " + radar.instance.pl_List[i].Call;
                plane_type.text = "plane type: " + radar.instance.pl_List[i].Type;






                break;
            }
        }


    }
}