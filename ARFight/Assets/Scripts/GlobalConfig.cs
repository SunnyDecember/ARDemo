using UnityEngine;
using System.Collections;
using Vuforia;

/* Author:       Running
** Time:         
** Describtion:  
*/

public class GlobalConfig : MonoBehaviour
{

    void Awake()
    {
        
    }
     
    void Start ()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    void Update() 
    {
        ModelManager.Instance.Update();
        Timer.Update();

        
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
#elif UNITY_ANDROID || UNITY_IPHONE
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
#endif
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}