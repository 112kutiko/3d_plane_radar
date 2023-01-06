using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_cam_hold : MonoBehaviour {
    public Camera plane_cam;
    public GameObject main_plane; 

    void Start() { plane_cam.enabled=false;}
    public void cam_play() {
        if (true != radar.instance.main_cam.enabled){
            if (plane_cam.enabled == false){radar.instance._now_plane = main_plane.GetComponent<plane_info>().Icao;}
            plane_cam.enabled = !plane_cam.enabled;
        }
}
    public void cam_back(){
        if (plane_cam.enabled==true){
            Debug.Log(plane_cam.enabled);
            radar.instance.main_cam_activator();
            radar.instance._now_plane = "";
        }
    }

}
