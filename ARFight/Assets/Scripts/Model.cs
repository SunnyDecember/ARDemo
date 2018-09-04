using UnityEngine;
using System.Collections;

/* Author:       Running
** Time:         18.9.4
** Describtion:  一个模型的管理
*/

public class Model : MonoBehaviour
{
    /// <summary>
    /// 模型的类型
    /// </summary>
    protected Type _modelType = Type.None;

    /// <summary>
    /// 模型初始的父节点。(原始的ImageTarget)
    /// </summary>
    [SerializeField]
    private GameObject _originImageTarget;

    /// <summary>
    /// 模型的动画器
    /// </summary>
    private Animator _animator;

    public enum Type 
    {
        None = 1 << 1,
        Animal = 1 << 2,         //动物(蛇，熊...)
        Environment = 1 << 4     //环境(比如树...)
    }

    void Awake()
    {
        if (null != GetComponent<Animator>())
        {
            _animator = GetComponent<Animator>();
        }
    }
     
    void Start ()
    {
        
    }
}