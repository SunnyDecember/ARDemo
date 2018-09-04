using UnityEngine;
using System.Collections;

/* Author:       Running
** Time:         18.9.4
** Describtion:  模型的管理
*/

public class ModelManager
{

    private static ModelManager _instance = null;

    public static ModelManager Instance
    {
        get 
        {
            if (null == _instance) 
            {
                _instance = new ModelManager();
            }
            return _instance;
        }
    }   

}