using UnityEngine;
using System.Collections;

/* Author		: Runing
** Time			: 18.7.11
** Describtion	: 操作Resources文件的帮助类
*/

public class ResourcesHelper
{
    private static ResourcesHelper _instance = null;

    public static ResourcesHelper Instance
    {
        get
        {
            return _instance ?? (_instance = new ResourcesHelper());
        }
    }

    /// <summary>
    /// 获取Resources文件夹下的预制体。（实例化一个GameObject然后GetComponent()）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">Resources里面的路径</param>
    /// <param name="parentTransform">父节点</param>
    /// <returns></returns>
    public T Get<T>(string path, Transform parentTransform) where T : Component
    {
        GameObject obj = Object.Instantiate(Resources.Load(path)) as GameObject;
        obj.transform.parent = parentTransform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        return obj.GetComponent<T>();
    }

    public GameObject Get(string path, Transform parentTransform)
    {
        GameObject obj = Object.Instantiate(Resources.Load(path)) as GameObject;
        obj.transform.parent = parentTransform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        return obj;
    }

    /// <summary>
    /// 更改所有子节点的层级
    /// </summary>
    /// <param name="tran"></param>
    public void ModifyLayer(Transform tran, int layerIndex) 
    {
        Transform[] array = tran.GetComponentsInChildren<Transform>();
        for (int i = 0; i < array.Length; i++)
        {
            Transform child = array[i];
            child.gameObject.layer = layerIndex;
        }
    }
}