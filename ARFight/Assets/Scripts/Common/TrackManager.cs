using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

/* Author:       Running
** Time:         18.9.4
** Describtion:  此类管理所有的ImageTarget.含有一个字典。
*/

public class TrackManager
{
    /// <summary>
    /// 是否展示模型出来。（通过按钮按下）
    /// </summary>
    public bool isShowModel = false;

    /// <summary>
    /// key是 ImageTarget 对象
    /// </summary>
    private Dictionary<GameObject, StatusData> _trackStatusDictionary = new Dictionary<GameObject, StatusData>();

    private static TrackManager _instance = null;

    class StatusData 
    {
        public TrackableBehaviour.Status previousStatus;
        public TrackableBehaviour.Status newStatus;
        public bool isFound;
    }

    public static TrackManager Instance
    {
        get 
        {
            if (null == _instance) 
            {
                _instance = new TrackManager();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 每次高通识别，都把状态记录起来。以便按钮按下可以进行恢复状态。
    /// </summary>
    /// <param name="imageTarget">为ImageTarget</param>
    /// <param name="previousStatus"></param>
    /// <param name="status"></param>
    public void SetTrackStatus(GameObject imageTarget, TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status status) 
    {
        DeleteNullObject();

        //判断是否识别到。
        bool isFound = false;
        if (status == TrackableBehaviour.Status.DETECTED ||
               status == TrackableBehaviour.Status.TRACKED ||
               status == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            isFound = true;
            EventCenter.Instance.PostEvent(EventName.ModelFound);
        }
        else
        {
            isFound = false;
            EventCenter.Instance.PostEvent(EventName.ModelLost);
        }

        //把当前ImageTarget状态记录起来。
        StatusData data = new StatusData();
        data.previousStatus = previousStatus;
        data.newStatus = status;
        data.isFound = isFound;

        //添加到字典中。
        if (_trackStatusDictionary.ContainsKey(imageTarget))
        {
            _trackStatusDictionary[imageTarget] = data;
        }
        else 
        {
            _trackStatusDictionary.Add(imageTarget, data);
        }

        //把模型添加到ModelManager中管理。
        Model[] modelArray = imageTarget.transform.GetComponentsInChildren<Model>(true);
        for (int i = 0; i < modelArray.Length; i++)
        {
            Model model = modelArray[i].gameObject.GetComponent<Model>();
            ModelManager.Instance.AddModel(model);
        }

        if (SceneData.Instance.type == SceneData.Type.SingleCard && isFound)
        {
            foreach (var kv in _trackStatusDictionary)
            {
                if (null != kv.Key && kv.Key != imageTarget)
                {
                    _trackStatusDictionary[kv.Key].isFound = false;
                }
            }
        }

        //如果没有识别到图片，这里就让模型消失吧。
        if (false == isFound)
            SetTrackStatus();
    }

    /// <summary>
    /// 移除多余的对象
    /// </summary>
    private void DeleteNullObject() 
    {
        List<GameObject> nullObject = new List<GameObject>();

        foreach (var kv in _trackStatusDictionary)
        {
            if(null == kv.Key)
            {
                nullObject.Add(kv.Key);
            }
        }

        for (int i = 0; i < nullObject.Count; i++)
        {
            _trackStatusDictionary.Remove(nullObject[i]);
        }
    }

    /// <summary>
    /// 每次高通识别丢失了,清除记录。
    /// </summary>
    /// <param name="obj"></param>
    //public void DeleteTrackStatus(GameObject obj) 
    //{
    //    if(_trackStatusDictionary.ContainsKey(obj))
    //    {
    //        _trackStatusDictionary.Remove(obj);
    //    }
    //}

    /// <summary>
    /// 按钮按下，恢复状态。
    /// </summary>
    public void SetTrackStatus() 
    {
        isShowModel = true;

        foreach(var kv in _trackStatusDictionary)
        {
            if (null != kv.Key)
            {
                GameObject imageTarget = kv.Key;

                //恢复状态
                if (imageTarget.GetComponent<DefaultTrackableEventHandler>()) 
                {
                    DefaultTrackableEventHandler handler = imageTarget.GetComponent<DefaultTrackableEventHandler>();
                    handler.MyTrackableStateChanged(kv.Value.isFound);
                    
                }
            }
        }

        //if (TrackerManager.Instance.GetTracker<ObjectTracker>() != null)
        //{
        //    TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        //    TrackerManager.Instance.GetTracker<ObjectTracker>().PersistExtendedTracking(false);
        //    Debug.Log("SHIBIEDA!脱离！");
        //}
     
        isShowModel = false;
    }

    //public void Separate() 
    //{
    //    foreach (var kv in _trackStatusDictionary)
    //    {
    //        if (null != kv.Key)
    //        {
    //            GameObject obj = kv.Key;
    //            {
    //                Model[] handler = obj.GetComponentsInChildren<Model>(true);
    //                for (int i = 0; i < handler.Length; i++)
    //                {
    //                    handler[i].transform.SetParent(null);
    //                }
    //            }
    //        }
    //    }
    //}
}