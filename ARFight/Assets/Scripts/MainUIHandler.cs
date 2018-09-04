using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* Author:       Running
** Time:         
** Describtion:  
*/

/// <summary>
/// 控制住场景中所有UI
/// </summary>
public class MainUIHandler : MonoBehaviour
{
    public Button button;

    public Button separate;
    void Awake()
    {
        
    }
     
    void Start ()
    {
        button.onClick.AddListener(() => 
        {
            TrackManager.Instance.ShowTrackStatus();
        });

        separate.onClick.AddListener(() => 
        {
            TrackManager.Instance.Separate();
        });
    }
}