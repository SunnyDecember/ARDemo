using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private Button _clearButton;

    [SerializeField]
    private Button _backButton;

    void Awake()
    {
       
    }
     
    void Start ()
    {
        recognition.onClick.AddListener(() => 
        {
            TrackManager.Instance.SetTrackStatus();
        });

        _clearButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

        _backButton.onClick.AddListener(() =>
        {
            SceneData.Instance.type = SceneData.Type.Menu;
            SceneManager.LoadScene("MenuScene");
        });
    }
   
}