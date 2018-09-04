using UnityEngine;
using System.Collections;

/* Author:       Running
** Time:         18.9.4
** Describtion:  动物模型(比如蛇，熊)
*/

public class AnimalModel : Model
{

    void Awake()
    {
        _modelType = Model.Type.Animal;
    }
     
    void Start ()
    {
        
    }
}