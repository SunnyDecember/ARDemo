using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventName
{
    /// <summary>
    /// 模型识别丢失
    /// </summary>
    public static readonly string ModelLost = "ModelLost";
    
    /// <summary>
    /// 识别到模型
    /// </summary>
    public static readonly string ModelFound = "ModelFound";

    /// <summary>
    /// 模型识别后的显示
    /// </summary>
    public static readonly string ModelShow = "ModelShow";

    /// <summary>
    /// 模型识别后的隐藏
    /// </summary>
    public static readonly string ModelHide = "ModelHide";

    /// <summary>
    /// 按钮按下，向左旋转模型
    /// </summary>
    public static readonly string TurnLeftDown = "TurnLeftDown";

    /// <summary>
    /// 按钮放开，向左旋转模型结束
    /// </summary>
    public static readonly string TurnLeftUp = "TurnLeftUp";

    /// <summary>
    /// 按钮按下，向右旋转模型
    /// </summary>
    public static readonly string TurnRightDown = "TurnRightDown";

    /// <summary>
    /// 按钮放开，向右旋转模型结束
    /// </summary>
    public static readonly string TurnRightUp = "TurnRightUp";
}
