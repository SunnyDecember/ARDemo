using UnityEngine;
using System.Collections;
using DG.Tweening;

/* Author:       Running
** Time:         18.9.4
** Describtion:  动物模型(比如蛇，熊)
*/

public class AnimalModel : Model
{
    [SerializeField]
    private Animator _animator;

    /// <summary>
    /// 模型旋转的速度
    /// </summary>
    private float _rotationSpeed = 1f;

    void Awake()
    {
        type = Model.Type.Animal;
    }
     
    void Start ()
    {
        //向右旋转按钮事件通知
        EventCenter.Instance.RegisterEvent(EventName.TurnRightDown, TurnRightDown);
        EventCenter.Instance.RegisterEvent(EventName.TurnRightUp, TurnRightUp);

        //向左旋转按钮事件通知
        EventCenter.Instance.RegisterEvent(EventName.TurnLeftDown, TurnLeftDown);
        EventCenter.Instance.RegisterEvent(EventName.TurnLeftUp, TurnLeftUp);
    }

    private void TurnRightDown(params object[] args)
    {
        if (gameObject.activeSelf)
            RotationTool.Instance.Add(gameObject, -_rotationSpeed);
    }

    private void TurnRightUp(params object[] args)
    {
        RotationTool.Instance.Delete(gameObject);
    }

    private void TurnLeftDown(params object[] args)
    {
        if (gameObject.activeSelf)
            RotationTool.Instance.Add(gameObject, _rotationSpeed);
    }

    private void TurnLeftUp(params object[] args)
    {
        RotationTool.Instance.Delete(gameObject);
    }

    /// <summary>
    /// 默认的destroy
    /// </summary>
    private void OnDestroy()
    {
        //移除侦听事件
        EventCenter.Instance.UnRegisterEvent(EventName.TurnRightDown, TurnRightDown);
        EventCenter.Instance.UnRegisterEvent(EventName.TurnRightUp, TurnRightUp);
        EventCenter.Instance.UnRegisterEvent(EventName.TurnLeftDown, TurnLeftDown);
        EventCenter.Instance.UnRegisterEvent(EventName.TurnLeftUp, TurnLeftUp);
    }

    public override void Action(Model model)
    {
        //如果动物已经有目标，那么不往下执行。
        if (true == _isHasTarget) return;

        //ModelNamager已经做了显示判断
        //如果自身还没激活。
        //if (false == gameObject.activeSelf) return;

        //如果对方不是环境，也不往下执行。
        if ((model.type & Model.Type.Environment) < 0) return;
        
        _isHasTarget = true;
        EnvironmentModel environmentModel = model as EnvironmentModel;
        transform.SetParent(environmentModel.originImageTarget.transform);

        if (environmentModel.placeList.Count > 0)
        {
            transform.DOMove(environmentModel.placeList[0].position, 4);
            transform.LookAt(environmentModel.placeList[0].position);
        
        if (null != _animator)
            _animator.SetBool("isSlithering", true);
        }

        //恢复动画
        Timer.Add(3.0f, (id, args) => 
        {
            if (null != _animator)
                _animator.SetBool("isSlithering", false);

            Timer.DeleteTimerWith(id);
        });
    }

    public override void End()
    {
        base.End();
        _isHasTarget = false;
        transform.localPosition = Vector3.zero;

        if (null != _animator)
            _animator.SetBool("isSlithering", false);
    }
}