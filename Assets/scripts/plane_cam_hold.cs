using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_cam_hold : MonoBehaviour
{
    public Camera plane_cam;
    public GameObject main_plane;
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
        if (true != radar.instance.main_cam.enabled)
        {
            radar.instance._now_plane = main_plane.GetComponent<plane_info>().Icao;
              plane_cam.enabled = !plane_cam.enabled;
        }
    }
    void OnDestroy()
    {
        if (plane_cam.enabled)
        {
            radar.instance.main_cam_activator();
        }
    }
}
