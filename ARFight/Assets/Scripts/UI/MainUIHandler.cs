using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* Author:       Running
** Time:         
** Describtion:  
*/

/// <summary>
/// 控制场景中所有UI
/// </summary>
public class MainUIHandler : MonoBehaviour
{
    /// <summary>
    /// 识别
    /// </summary>
    public Button recognition;

    public Button separate;
    
    void Awake()
    {
       
    }
     
    void Start ()
    {
        recognition.onClick.AddListener(() => 
        {
            TrackManager.Instance.SetTrackStatus();
        });
    }
   
}