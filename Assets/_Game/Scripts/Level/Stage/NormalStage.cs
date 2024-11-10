using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStage : Stage
{
    [SerializeField] int stageDuration = 5;

    public override void InitStage()
    {
        Action<int> onTick = UIManager.Ins.GetUI<UIGameplay>().UpdateCountdown;
        Action onComplete = () => OnStageFinish();

        CountdownManager.Ins.StartCountdown(stageDuration, onTick, onComplete);
    }

    public virtual void OnStageFinish()
    {
        this.PostEvent(EventID.OnStageFinish, isStageWin);
    }
}
