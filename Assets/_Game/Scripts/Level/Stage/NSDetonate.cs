using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSDetonate : NormalStage
{
    [SerializeField] Collider2D firework, square;
    [SerializeField] float moveSpeed = 10f;
    private int chance = 1;
    private bool isMoving = false;
    private Transform fireworkTF;

    void Update()
    {
        if (isMoving)
        {
            fireworkTF.position += moveSpeed * Time.deltaTime * Vector3.up;
        }
    }

    public override void InitStage()
    {
        base.InitStage();
        float waitDuration = Random.Range(1f, 3f);
        StartCoroutine(WaitBeforeFall(waitDuration));
        fireworkTF = firework.transform;
        float randomScale = Random.Range(0.5f, 1f);
        square.transform.localScale *= randomScale;
    }

    protected override void OnPointerDownActionHandle(PressButtonType type)
    {
        if (type == PressButtonType.A && isMoving && chance > 0)
        {
            chance--;

            isMoving = false;
            if (firework.IsTouching(square))
            {
                isStageWin = true;
            }
        }
    }

    IEnumerator WaitBeforeFall(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMoving = true;
    }

    public override void OnStageFinish()
    {
        base.OnStageFinish();
        isMoving = false;
        chance = 0;
    }
}
