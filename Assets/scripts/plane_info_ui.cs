using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class plane_info_ui : MonoBehaviour{
    public static plane_info_ui instance;
    public GameObject panel,img_full;
    public IdList plane_is;
    public Text plane_id, plane_reg,plane_Icao,plane_call,plane_type,plane_mdl,plane_from,plane_to,plane_op,plane_alt,plane_spd,plane_lat,plane_long,plane_track,plane_mill;
    public bool on_off = false;
    public string _tmp_plane = string.Empty;
    public Image plane_main_image;
    private bool flag = false;


    void Start() { instance = this; }

    void Update()
    {
        if (radar.instance._now_plane != "" && radar.instance.now_use_cam_id != -1)
        {
            on_off = true;
            on_info(on_off);
            _tmp_plane = radar.instance._now_plane;
            plane_main_image = null;
            if (radar.instance.now_use_cam_id != -1) { set_info(); img_full.SetActive(true); }
        }
        else
        {
            on_off = false;
            on_info(on_off);
            _tmp_plane = "";
        }
        if (on_off && !flag && radar.instance._now_plane != "")
        {
            flag = true;
            if (radar.instance.now_use_cam_id != -1) { set_info(); img_full.SetActive(true); }
        }
        if (on_off && flag && radar.instance._now_plane == "")
        {
            flag = false;
        }
    }

    public void on_info(bool i) { panel.SetActive(i); }

public void set_info()  {
    IdList obj = radar.instance.pl_List.Find(o => o.Icao == _tmp_plane);
    if (obj != null)  {  
        plane_is = obj;  
    }
    else{
        Debug.Log("[ERROR] PLANE not found");   
    }

    if (plane_is != null) {
        plane_id.text = "id: " + plane_is.Id;
        plane_reg.text = "registration: " + plane_is.Reg;
        plane_Icao.text = "ICao: " + plane_is.Icao;
        plane_call.text = "Call: " + plane_is.Call;
        plane_type.text = "plane type: " + plane_is.Type;
        plane_mdl.text="plane full model name: "+ plane_is.Mdl;
        plane_from.text="from: "+ plane_is.From;
        plane_to.text="To: "+ plane_is.To;
        plane_op.text="Operator: " + plane_is.Op;
        plane_alt.text= "Alt (km): "+ plane_is.Alt*10;
        plane_spd.text = "speed: " + plane_is.Spd;
        plane_lat.text="Lat: "+ plane_is.Lat;           
        plane_long.text="Long: " + plane_is.Long;            
        plane_track.text = "Direction: " + plane_is.Trak;
        plane_mill.text= "military: " + plane_is.Mil;
        if (plane_is.plane.GetComponent<plane_info>().ats == "200" && plane_main_image==null) {
            img_full.SetActive(true);
            string url = plane_is.plane.GetComponent<plane_info>().link_img;
            StartCoroutine(GetImageFromWeb(url));
        } else{
            img_full.SetActive(false);
        }
    }
}

    IEnumerator GetImageFromWeb(string x){
        UnityWebRequest reg = UnityWebRequestTexture.GetTexture(x);
        yield return reg.SendWebRequest();
        if (reg.error == null) {
             Texture2D img =  ((DownloadHandlerTexture)reg.downloadHandler).texture;
            plane_main_image.sprite = Sprite.Create(img, new Rect(0f, 0f, img.width, img.height),Vector2.zero);
        }  else { Debug.Log("fail  plane id:"+ plane_is.Icao);   }
    }
}