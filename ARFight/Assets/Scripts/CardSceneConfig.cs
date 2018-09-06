using UnityEngine;
using System.Collections;
using Vuforia;

/* Author:       Running
** Time:         18.9.6
** Describtion:  配置卡牌场景
*/

public class CardSceneConfig : MonoBehaviour
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