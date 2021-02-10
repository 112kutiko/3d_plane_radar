using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class free_cam_control : MonoBehaviour
{
    public float speed = 6.0F;
    private Vector3 moveDirection = Vector3.zero;
    public float mouseSensitivity = 100f;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (radar.instance._now_plane == "" || radar.instance._now_plane == string.Empty)
        {
            float h = 2 * Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            transform.Rotate(0, h, 0);
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

    }
}
