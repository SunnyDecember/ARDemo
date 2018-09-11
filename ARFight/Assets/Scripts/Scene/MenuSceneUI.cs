using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/* Author:       Running
** Time:         18.9.6
** Describtion:  主菜单的UI
*/

public class MenuSceneUI : MonoBehaviour
{
    /// <summary>
    /// 单卡按钮
    /// </summary>
    [SerializeField]
    private Button _singleCardButton;

    /// <summary>
    /// 多卡按钮
    /// </summary>
    [SerializeField]
    private Button _multiCardButton;

    /// <summary>
    /// 绘画模型
    /// </summary>
    [SerializeField]
    private Button _drawModleButton;

    void Awake() 
    {
        SceneData.Instance.type = SceneData.Type.Menu;
    }

    void Start ()
    {
        _singleCardButton.onClick.AddListener(() => 
        {
            SceneData.Instance.type = SceneData.Type.SingleCard;
            SceneManager.LoadScene("MainScene");
        });

        _multiCardButton.onClick.AddListener(() =>
        {
            SceneData.Instance.type = SceneData.Type.MultiCard;
            SceneManager.LoadScene("MainScene");
        });

        _drawModleButton.onClick.AddListener(() =>
        {
            SceneData.Instance.type = SceneData.Type.DrawModle;
            SceneManager.LoadScene("DrawScene");
        });
    }
}