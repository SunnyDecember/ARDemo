using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

/* Author:       Running
** Time:         
** Describtion:  
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

    struct StatusData 
    {
        public TrackableBehaviour.Status previousStatus;
        public TrackableBehaviour.Status newStatus;
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


    public void Init() 
    {
        
    }

   

    /// <summary>
    /// 每次高通识别，都把状态记录起来。以便按钮按下可以进行恢复状态。
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="previousStatus"></param>
    /// <param name="status"></param>
    public void SetTrackStatus(GameObject obj, TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status status) 
    {
        StatusData data = new StatusData();
        data.previousStatus = previousStatus;
        data.newStatus = status;

        if (_trackStatusDictionary.ContainsKey(obj))
        {
            _trackStatusDictionary[obj] = data;
        }
        else 
        {
            _trackStatusDictionary.Add(obj, data);
        }
        
    }

    /// <summary>
    /// 每次高通识别丢失了,清除记录。
    /// </summary>
    /// <param name="obj"></param>
    public void DeleteTrackStatus(GameObject obj) 
    {
        if(_trackStatusDictionary.ContainsKey(obj))
        {
            _trackStatusDictionary.Remove(obj);
        }
    }

    /// <summary>
    /// 按钮按下，恢复状态。
    /// </summary>
    public void ShowTrackStatus() 
    {
        isShowModel = true;
        foreach(var kv in _trackStatusDictionary)
        {
            if (null != kv.Key)
            {
                GameObject obj = kv.Key;
                if (obj.GetComponent<DefaultTrackableEventHandler>()) 
                {
                    DefaultTrackableEventHandler handler = obj.GetComponent<DefaultTrackableEventHandler>();
                    handler.MyTrackableStateChanged(kv.Value.previousStatus, kv.Value.newStatus);
                    
                }
            }
        }

        if (TrackerManager.Instance.GetTracker<ObjectTracker>() != null)
        {
            //TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
            //TrackerManager.Instance.GetTracker<ObjectTracker>().PersistExtendedTracking(false);
            Debug.Log("SHIBIEDA!脱离！");
        }
     
        isShowModel = false;
    }

    public void Separate() 
    {
        foreach (var kv in _trackStatusDictionary)
        {
            if (null != kv.Key)
            {
                GameObject obj = kv.Key;
                {
                    Model[] handler = obj.GetComponentsInChildren<Model>(true);
                    for (int i = 0; i < handler.Length; i++)
                    {
                        handler[i].transform.SetParent(null);
                    }
                }
            }
        }
    }
}