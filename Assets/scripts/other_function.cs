using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.SceneManagement;

public class other_function : MonoBehaviour
{
    public static other_function stats;
    public  Vector3 mp;
    public bool is_on_area;

    public GameObject pause_ui;

    void Start(){ stats = this;}
    void Update(){
        Vector3 mousePos_get = Input.mousePosition;
        mp = mousePos_get;
        check(mp);
        if (Input.GetKeyDown(KeyCode.Escape)){
            pause_ui.SetActive(!pause_ui.activeSelf);
        }
    }
    void check(Vector3 mousePos) {
        if(mousePos.x<0|| mousePos.y<0||mousePos.x > Screen.width || mousePos.y > Screen.height) {
            is_on_area = false;   }  else    { 
            if(pause_ui.activeSelf==false){is_on_area = true; }
        }
    }
    public void change_ps(){pause_ui.SetActive(!pause_ui.activeSelf);}
    public void back_m() {SceneManager.LoadScene(0);}
}
