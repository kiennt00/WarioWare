using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSChooseDifferent : NormalStage
{
    [System.Serializable]
    public class Couple
    {
        public Sprite sprite1;
        public Sprite sprite2;
    }

    [SerializeField] List<Couple> listCouple = new();

    [SerializeField] List<Sprite> listCard;

    public override void InitStage()
    {
        base.InitStage();
    }

    protected override void OnPointerDownActionHandle(PressButtonType type)
    {
        if (true)
        {
            if (type == PressButtonType.Left)
            {

            }
            else if (type == PressButtonType.Right)
            {

            }
            else if (type == PressButtonType.A)
            {

            }
        }
    }

    public override void OnStageFinish()
    {
        base.OnStageFinish();
    }
}

