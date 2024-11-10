using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    protected bool isStageWin = false;

    Action<object> _OnPressAction;
    Action<object> _OnPointerDownAction;
    Action<object> _OnPointerUpAction;

    protected void Awake()
    {
        _OnPressAction = (param) => { OnPressActionHandle((PressButtonType)param); };
        _OnPointerDownAction = (param) => { OnPointerDownActionHandle((PressButtonType)param); };
        _OnPointerUpAction = (param) => { OnPointerUpActionHandle((PressButtonType)param); };

        this.RegisterListener(EventID.OnPressAction, _OnPressAction);
        this.RegisterListener(EventID.OnPointerDownAction, _OnPointerDownAction);
        this.RegisterListener(EventID.OnPointerUpAction, _OnPointerUpAction);
    }   

    protected virtual void OnPressActionHandle(PressButtonType type)
    {

    }

    protected virtual void OnPointerDownActionHandle(PressButtonType type)
    {

    }

    protected virtual void OnPointerUpActionHandle(PressButtonType type)
    {

    }

    public virtual void InitStage()
    {
        
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        EventDispatcher.Ins.RemoveListener(EventID.OnPressAction, _OnPressAction);
        EventDispatcher.Ins.RemoveListener(EventID.OnPointerDownAction, _OnPointerDownAction);
        EventDispatcher.Ins.RemoveListener(EventID.OnPointerUpAction, _OnPointerUpAction);
    }
}
