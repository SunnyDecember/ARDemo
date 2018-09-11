using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:       Running
** Time:         18.9.11
** Describtion:  旋转工具
*/

public class RotationTool
{
    private Dictionary<GameObject, float> _rotationObject = new Dictionary<GameObject, float>();

    private static RotationTool _instance = null;

    public static RotationTool Instance
    {
        get
        {
            return _instance ?? (_instance = new RotationTool());
        }
    }

    //初始化
    public void Init()
    {
        Timer.Add(-1, (id, args)=> { Update(); });
    }

    /// <summary>
    /// 添加需要做旋转的物体
    /// </summary>
    /// <param name="modelRoot"></param>
    /// <param name="speed"></param>
    public void Add(GameObject modelRoot, float speed)
    {
        if (!_rotationObject.ContainsKey(modelRoot))
        {
            _rotationObject.Add(modelRoot, speed);
        }
        else
        {
            _rotationObject[modelRoot] = speed;
        }
    }

    /// <summary>
    /// 删除旋转的物体
    /// </summary>
    /// <param name="modelRoot"></param>
    public void Delete(GameObject modelRoot)
    {
        if (_rotationObject.ContainsKey(modelRoot))
        {
            _rotationObject.Remove(modelRoot);
        }
    }

    /// <summary>
    /// 每帧更新旋转的物体
    /// </summary>
    private void Update()
    {
        foreach (var kv in _rotationObject)
        {
            if (null != kv.Key)
            {
                GameObject modelRoot = kv.Key;
                float speed = kv.Value;
                modelRoot.transform.Rotate(new Vector3(0, 1, 0), 1 * speed);
            }
        }
    }
}
