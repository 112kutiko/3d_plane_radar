using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class free_cam_control : MonoBehaviour
{
    private float movementSpeed = 4f;
    public float mouseSensitivity = 100f;
    private Camera cam;

    public float climbSpeed = 4f;
    public float n_speed = 10f;
    public float slow_move_factor = 0.25f;
    public float fast_move_factor = 3f;

    ///private float rotationX=0f, rotationY=0f;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Screen.lockCursor = true;
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
                #region move

                if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                transform.Translate(horizontalInput * movementSpeed * Time.deltaTime, 0, verticalInput * movementSpeed * Time.deltaTime);
                #endregion
            }
        }
    }
}
