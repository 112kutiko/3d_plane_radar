using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class free_cam_view : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float xr_1 = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (other_function.stats.is_on_area)
        {
        if (radar.instance._now_plane==""|| radar.instance._now_plane == string.Empty)
                {
                    float y = 2 * Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                    xr_1 -= y;
                    xr_1 = Mathf.Clamp(xr_1, -90f, 63f);
                    transform.localRotation = Quaternion.Euler(xr_1, 0f, 0f);
                }
        }
        

    }

    

}
