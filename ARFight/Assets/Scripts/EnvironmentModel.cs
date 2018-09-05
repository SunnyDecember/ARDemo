using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Author:       Running
** Time:         18.9.4
** Describtion:  环境模型(比如树)
*/

public class EnvironmentModel : Model
{
    public List<Transform> placeList = new List<Transform>();

    void Awake()
    {
        type = Model.Type.Environment;
    }
     
    void Start ()
    {
        
    }

    public override void Action(Model model) 
    {
        if ((model.type & Model.Type.Animal) > 0)
        {
            _isHasTarget = true;
        }
    }

    public override void End()
    {
        base.End();
        _isHasTarget = false;
    }
}