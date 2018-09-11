using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.SceneManagement;

/* Author:       Running
** Time:         18.9.6
** Describtion:  配置卡牌场景
*/

public class GlobalConfig : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
     
    void Start ()
    {
        //CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        RotationTool.Instance.Init();

        SceneManager.LoadScene("MenuScene");
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
            //CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}