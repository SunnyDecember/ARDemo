using UnityEngine;
using System.Collections;

/* Author:       Running
** Time:         18.9.4
** Describtion:  一个模型的管理
*/

public class Model : MonoBehaviour
{
    /// <summary>
    /// 是否有目标。（比如动物和树靠在了一起，那么两者都有目标, 动物向树走去）
    /// </summary>
    protected bool _isHasTarget = false;

    /// <summary>
    /// 模型的类型
    /// </summary>
    [HideInInspector]
    public Type type = Type.None;

    /// <summary>
    /// 模型初始的父节点。(原始的ImageTarget)
    /// </summary>
    public GameObject originImageTarget;

    public enum Type 
    {
        None = 1 << 1,
        Animal = 1 << 2,         //动物(蛇，熊...)
        Environment = 1 << 4     //环境(比如树...)
    }

    /// <summary>
    /// 有所行动。(比如一定距离内动物遇到了树，会向树走)
    /// </summary>
    public virtual void Action(Model model) {}

    /// <summary>
    /// 识别丢失，可能需要恢复数据。(比如重新设置父节点和位置)
    /// </summary>
    public virtual void End() 
    {
        transform.SetParent(originImageTarget.transform);
    }
}