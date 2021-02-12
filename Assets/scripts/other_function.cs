using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class other_function : MonoBehaviour
{
    public static other_function stats;
    public  Vector3 mp;
    public bool is_on_area;
    // Start is called before the first frame update
    void Start()
    {
        stats = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos_get = Input.mousePosition;
        mp = mousePos_get;
        check(mp);
    }
    void check(Vector3 mousePos)
    {
        if(mousePos.x<0|| mousePos.y<0||mousePos.x > Screen.width || mousePos.y > Screen.height)
        {
           // Debug.Log("______________________________________");
           // Debug.Log("mouse x: "+mousePos.x +" y: "+mousePos.y);
           // Debug.Log("______________________________________");
            is_on_area = false;
        }
        else
        {
            is_on_area = true;
        }
    }


}
