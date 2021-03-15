using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class plane_info_ui : MonoBehaviour
{
    public static plane_info_ui instance;
    public GameObject panel,img_full,plane_is;
    public Text plane_id, plane_reg,plane_Icao,plane_call,plane_type,plane_mdl,plane_from,plane_to,plane_op,plane_alt,plane_spd,plane_lat,plane_long,plane_track,plane_mill;
    public bool on_off = false;
    public string _tmp_plane = string.Empty;
    public Image plane_main_image;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(radar.instance._now_plane!="" && radar.instance.now_use_cam_id != -1) { 
            on_off = true; 
            on_info(on_off);
            _tmp_plane = radar.instance._now_plane;
            if (radar.instance.now_use_cam_id != -1) { set_info(); img_full.SetActive(true); }
        } else { 
            on_off = false; 
            on_info(on_off);
            _tmp_plane = "";
        }
        if (on_off)
        {
            if (radar.instance.now_use_cam_id != -1) { set_info(); img_full.SetActive(true); }
        }
    }
    public void on_info(bool i) { panel.SetActive(i); }

    public void set_info()
    {
        plane_is= GameObject.Find("CitySimulatorMap/"+ _tmp_plane);
        plane_info in_plane = plane_is.GetComponent<plane_info>();

        if (plane_is != null)
        {
            plane_id.text = "id: " + in_plane.Id;
            plane_reg.text = "registration: " + in_plane.Reg;
            plane_Icao.text = "ICao: " + in_plane.Icao;
            plane_call.text = "Call: " + in_plane.Call;
            plane_type.text = "plane type: " + in_plane.Type;
            plane_mdl.text="plane full model name: "+ in_plane.Mdl;
            plane_from.text="from: "+ in_plane.From;
            plane_to.text="To: "+ in_plane.To;
            plane_op.text="Operator: " + in_plane.Op;
            plane_alt.text= "Alt (km): "+ in_plane.Alt;
            plane_spd.text = "speed: " + in_plane.Spd;
            plane_lat.text="Lat: "+ in_plane.Lat;           
            plane_long.text="Long: " + in_plane.Long;            
            plane_track.text = "Direction: " + in_plane.Trak;
            plane_mill.text= "military: " + in_plane.Mil;
            if (in_plane.ats == "200")
            {
                img_full.SetActive(true);
                string url = in_plane.link_img;
                StartCoroutine(GetImageFromWeb(url));
            }
            else
            {
                img_full.SetActive(false);
            }
        }
    }
    IEnumerator GetImageFromWeb(string x)
    {
        UnityWebRequest reg = UnityWebRequestTexture.GetTexture(x);
        yield return reg.SendWebRequest();
        if (reg.error == null)
        {
             Texture2D img =  ((DownloadHandlerTexture)reg.downloadHandler).texture;
            plane_main_image.sprite = Sprite.Create(img, new Rect(0f, 0f, img.width, img.height),Vector2.zero);
        }
        else
        { Debug.Log("fail  plane id:"+ plane_is.GetComponent<plane_info>().Icao);
          
        }
    }


}
