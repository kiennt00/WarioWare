using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSCatchStick : NormalStage
{
    [SerializeField] Collider2D hand, stick;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject handWait, handCatch;
    [SerializeField] GameObject handHold, handDrop;
    private int chance = 1;
    private bool isMoving = false;
    private Transform stickTF;

    private void Update()
    {
        if (isMoving)
        {
            stickTF.position += moveSpeed * Time.deltaTime * Vector3.down;
        }
    }

    public override void InitStage()
    {
        base.InitStage();
        float waitDuration = Random.Range(1f, 3f);
        StartCoroutine(WaitBeforeFall(waitDuration));
        stickTF = stick.transform;
        float randomScale = Random.Range(0.5f, 1f);
        Vector3 scale = stickTF.localScale;
        scale.y *= randomScale;
        stickTF.localScale = scale;
    }

    protected override void OnPointerDownActionHandle(PressButtonType type)
    {
        if (type == PressButtonType.A && chance > 0)
        {
            chance--;
            handWait.SetActive(false);
            handCatch.SetActive(true);

            if (hand.IsTouching(stick))
            {
                isStageWin = true;
                isMoving = false;
            }
        }
    }

    IEnumerator WaitBeforeFall(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMoving = true;
        handHold.SetActive(false);
        handDrop.SetActive(true);
    }

    public override void OnStageFinish()
    {
        base.OnStageFinish();
        isMoving = false;
        chance = 0;
    }
}
