using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;

/* Author:       Running
** Time:         18.9.11
** Describtion:  
*/

public class DrawScene : MonoBehaviour
{
    [SerializeField]
    private BloomOptimized _bloomOptimized;

    void Awake()
    {
        _bloomOptimized.enabled = false;

        //模型显示或隐藏在卡片上
        EventCenter.Instance.RegisterEvent(EventName.ModelShow, ModelShow);
        EventCenter.Instance.RegisterEvent(EventName.ModelHide, ModelHide);
    }
     
    /// <summary>
    /// 模型展示的时候，主要有模型显示出来，那么就开启BloomOptimized组件。
    /// </summary>
    /// <param name="args"></param>
    private void ModelShow(params object[] args)
    {
        HashSet<Model> modelHash = ModelManager.Instance.AllModelList;
        foreach (var model in modelHash)
        {
            if(model.gameObject.activeSelf)
            {
                _bloomOptimized.enabled = true;
                return;
            }
        }
    }

    private void ModelHide(params object[] args)
    {
        bool isShowHighLighting = false;
        HashSet<Model> modelHash = ModelManager.Instance.AllModelList;

        foreach (var model in modelHash)
        {
            if (model.gameObject.activeSelf)
            {
                isShowHighLighting = true;
                break;
            }
        }

        _bloomOptimized.enabled = isShowHighLighting;
    }

    private void OnDestroy()
    {
        EventCenter.Instance.UnRegisterEvent(EventName.ModelShow, ModelShow);
        EventCenter.Instance.UnRegisterEvent(EventName.ModelHide, ModelHide);
    }
}