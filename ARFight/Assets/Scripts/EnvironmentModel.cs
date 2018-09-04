using UnityEngine;
using System.Collections;

/* Author:       Running
** Time:         18.9.4
** Describtion:  环境模型(比如树)
*/

public class EnvironmentModel : Model
{

    void Awake()
    {
        _modelType = Model.Type.Environment;
    }
     
    void Start ()
    {
        
    }
}