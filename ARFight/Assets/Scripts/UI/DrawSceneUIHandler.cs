using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Author:       Running
** Time:         
** Describtion:  
*/

/// <summary>
/// 控制住场景中所有UI
/// </summary>
public class DrawSceneUIHandler : MonoBehaviour
{
    public Button recognition;

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

    public Transform referenceMap;

    [SerializeField]
    private Button _clearButton;

    [SerializeField]
    private Button _backButton;

    private P3D_ClickToPaint _script;
     
    void Awake()
    {
        SceneData.Instance.type = SceneData.Type.DrawModle;

        //模型显示或隐藏在卡片上
        EventCenter.Instance.RegisterEvent(EventName.ModelShow, ModelShow);
        EventCenter.Instance.RegisterEvent(EventName.ModelHide, ModelHide);
    }
     
    void Start ()
    {
        _script = Camera.main.GetComponent<P3D_ClickToPaint>();
        ColorSize.value = _script.Brush.Size.x / 100 + 0.01f;

        recognition.onClick.AddListener(() => 
        {
            TrackManager.Instance.SetTrackStatus();
        });

        ColorRed.onClick.AddListener(() =>
        {
            _script.Brush.Color = Color.red;
        });

        ColorGreen.onClick.AddListener(() =>
        {
            _script.Brush.Color = Color.green;
        });

        ColorYellow.onClick.AddListener(() =>
        {
            _script.Brush.Color = Color.yellow;
        });

        ColorOrange.onClick.AddListener(() =>
        {
            _script.Brush.Color = new Color(0.8f, 0.5f, 0);
        });

        ColorBlue.onClick.AddListener(() =>
        {
            _script.Brush.Color = Color.blue;
        });

        ColorBlack.onClick.AddListener(() =>
        {
            _script.Brush.Color = Color.black;
        });
        
        Rubber.onClick.AddListener(() =>
        {
            _script.Brush.Color = Color.white;
        });

        _clearButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("DrawScene");
        });

        _backButton.onClick.AddListener(() =>
        {
            SceneData.Instance.type = SceneData.Type.Menu;
            SceneManager.LoadScene("MenuScene");
        });
        
        ColorSize.onValueChanged.AddListener((float value) =>
        {
            
            _script.Brush.Size.x = Mathf.Clamp01(value + 0.01f) * 100;
            _script.Brush.Size.y = Mathf.Clamp01(value + 0.01f) * 100;
        });
        // item.onValueChanged.AddListener((float value) =>);

        //向右旋转模型
        UGUIEventListener.Get(TurnRight.gameObject).onPointDown = go => 
        {
            EventCenter.Instance.PostEvent(EventName.TurnRightDown);
        };

        UGUIEventListener.Get(TurnRight.gameObject).onPointUp = go =>
        {
            EventCenter.Instance.PostEvent(EventName.TurnRightUp);
        };

        //向左旋转模型
        UGUIEventListener.Get(TurnLeft.gameObject).onPointDown = go =>
        {
            EventCenter.Instance.PostEvent(EventName.TurnLeftDown);
        };

        UGUIEventListener.Get(TurnLeft.gameObject).onPointUp = go =>
        {
            EventCenter.Instance.PostEvent(EventName.TurnLeftUp);
        };

        //separate.onClick.AddListener(() => 
        //{
        //    TrackManager.Instance.Separate();
        //});
    }

    private void ModelShow(params object[] args)
    {
        string name = args[0] as string;

        //因为每次只显示一个模型的参考图，所以这里会删除以前旧的。
        Transform[] transforms = referenceMap.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            Transform tran = transforms[i];
            if (tran != referenceMap)
                GameObject.Destroy(tran.gameObject);
        }

        GameObject modelMap = ResourcesHelper.Instance.Get("ReferenceMap/" + name, referenceMap);
        modelMap.name = name;
        modelMap.transform.localPosition = new Vector3(0, -80, 0);
        modelMap.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        modelMap.transform.localScale = new Vector3(30, 30, 30);
        ResourcesHelper.Instance.ModifyLayer(modelMap.transform, 5);
    }

    private void ModelHide(params object[] args)
    {
        string name = args[0] as string;

        //删除同名的参考图模型
        Transform[] transforms = referenceMap.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            Transform tran = transforms[i];

            if(tran.name.Trim() == name.Trim() && (tran != referenceMap))
                GameObject.Destroy(tran.gameObject);
        }
    }

    private void OnDestroy()
    {
        EventCenter.Instance.UnRegisterEvent(EventName.ModelShow, ModelShow);
        EventCenter.Instance.UnRegisterEvent(EventName.ModelHide, ModelHide);
    }
}