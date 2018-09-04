using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Author:       Running
** Time:         18.9.4
** Describtion:  环境模型(比如树)
*/

public class EnvironmentModel : Model
{
    [SerializeField]
    private List<Transform> _placeList = new List<Transform>();

    void Awake()
    {
        _modelType = Model.Type.Environment;
    }
     
    void Start ()
    {
        
    }
}