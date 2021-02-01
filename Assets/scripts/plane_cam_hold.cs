using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_cam_hold : MonoBehaviour
{
    public Camera plane_cam;
    public GameObject cam_p;
  
    // Start is called before the first frame update
    void Start()
    {
        plane_cam.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void cam_play()
    {
        plane_cam.enabled = !plane_cam.enabled;
        cam_p.SetActive(radar.instance.ipy);
        radar.instance.ipy = !radar.instance.ipy;
    }
}
