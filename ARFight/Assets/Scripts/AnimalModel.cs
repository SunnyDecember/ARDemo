using UnityEngine;
using System.Collections;
using DG.Tweening;

/* Author:       Running
** Time:         18.9.4
** Describtion:  动物模型(比如蛇，熊)
*/

[RequireComponent(typeof(Animator))]
public class AnimalModel : Model
{
    private Animator _animator;

    void Awake()
    {
        type = Model.Type.Animal;
        _animator = transform.GetComponent<Animator>();
    }
     
    void Start ()
    {
        
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