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
public class DrawModUIIHandler : MonoBehaviour
{
    public Button button;

    public Button separate;

    public Button TurnRight;

    public Button TurnLeft;

    public Button ColorRed;

    public Button ColorGreen;

    public Button ColorYellow;

    public Button ColorOrange;

    public Button ColorBlue;

    public Button ColorBlack;

    public Button Rubber;//橡皮擦

    public Slider ColorSize;

    private P3D_ClickToPaint script;
     
    void Awake()
    {
        SceneData.Instance.type = SceneData.Type.DrawModle;
    }
     
    void Start ()
    {
        script = Camera.main.GetComponent<P3D_ClickToPaint>();
        ColorSize.value = script.Brush.Size.x / 100 + 0.01f;
        button.onClick.AddListener(() => 
        {
            TrackManager.Instance.SetTrackStatus();
        });
        ColorRed.onClick.AddListener(() =>
        {
            script.Brush.Color = Color.red;
        });

        ColorGreen.onClick.AddListener(() =>
        {
            script.Brush.Color = Color.green;
        });
        ColorYellow.onClick.AddListener(() =>
        {
            script.Brush.Color = Color.yellow;
        });
        ColorOrange.onClick.AddListener(() =>
        {
            script.Brush.Color = new Color(0.8f, 0.5f, 0);
        });
        ColorBlue.onClick.AddListener(() =>
        {
            script.Brush.Color = Color.blue;
        });
        ColorBlack.onClick.AddListener(() =>
        {
            script.Brush.Color = Color.black;
        });




        Rubber.onClick.AddListener(() =>
        {
            script.Brush.Color = Color.white;
        });



        ColorSize.onValueChanged.AddListener((float value) =>
        {
            
            script.Brush.Size.x = Mathf.Clamp01(value + 0.01f) * 100;
            script.Brush.Size.y = Mathf.Clamp01(value + 0.01f) * 100;
        });
       // item.onValueChanged.AddListener((float value) =>);


  

   
        //separate.onClick.AddListener(() => 
        //{
        //    TrackManager.Instance.Separate();
        //});
    }
   
}