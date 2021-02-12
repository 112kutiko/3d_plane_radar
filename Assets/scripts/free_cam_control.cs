using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class free_cam_control : MonoBehaviour
{
    private float movementSpeed = 4f;
    public float mouseSensitivity = 100f;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
       // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (other_function.stats.is_on_area)
        {
            if (radar.instance._now_plane == "" || radar.instance._now_plane == string.Empty)
            {
                float h = 2 * Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                transform.Rotate(0, h, 0);
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                transform.Translate(horizontalInput * movementSpeed * Time.deltaTime, 0, verticalInput * movementSpeed * Time.deltaTime);
            }
        }
    }
}
