using UnityEngine;
using System.Collections;

/* Author:       Running
** Time:         18.9.6
** Describtion:  
*/

public class SceneData
{
    public Type type = Type.None;

    private static SceneData _instance = null;

    public static SceneData Instance
    {
        get
        {
            return _instance ?? (_instance = new SceneData());
        }
    }

    public enum Type 
    {
        None,    
        Menu,           //菜单场景
        SingleCard,     //单卡模型
        MultiCard,      //多卡模型
        DrawModle       //绘画模型
    }
}