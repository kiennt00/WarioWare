using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSGolf : NormalStage
{
    [SerializeField] Collider2D ball, hole;
    [SerializeField] Transform guideline;
    [SerializeField] List<Transform> listHolePos = new();
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotateSpeed = 60f;
    private int chance = 1;
    private bool isMoving = false;
    private Vector3 dir = Vector3.zero;
    private Transform ballTF;

    void Update()
    {
        if (isMoving)
        {
            ballTF.position += moveSpeed * Time.deltaTime * dir;

            if (ball.IsTouching(hole))
            {
                ball.gameObject.SetActive(false);
                isMoving = false;
                isStageWin = true;
            }
        }
    }

    public override void InitStage()
    {
        base.InitStage();
        int randomIndex = Random.Range(0, listHolePos.Count);
        hole.transform.position = listHolePos[randomIndex].position;
        ballTF = ball.transform;
    }

    protected override void OnPointerDownActionHandle(PressButtonType type)
    {
        if (type == PressButtonType.A && chance > 0)
        {
            chance--;

            float angle = guideline.eulerAngles.z;
            float angleInRadians = angle * Mathf.Deg2Rad;
            dir = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f);
            isMoving = true;
            guideline.gameObject.SetActive(false);
        }
    }

    protected override void OnPressActionHandle(PressButtonType type)
    {
        if (chance > 0)
        {
            //if (type == PressButtonType.Up && guideline.eulerAngles.z > 110f)
            //{
            //    guideline.rotation *= Quaternion.Euler(0f, 0f, -rotateSpeed * Time.deltaTime);
            //}
            //else if (type == PressButtonType.Down && guideline.eulerAngles.z < 250f)
            //{
            //    guideline.rotation *= Quaternion.Euler(0f, 0f, rotateSpeed * Time.deltaTime);
            //}

            float zRot = guideline.eulerAngles.z;
            if (type == PressButtonType.Up)
            {
                zRot = Mathf.Clamp(zRot - rotateSpeed * Time.deltaTime, 110f, 250f);
            }
            else if (type == PressButtonType.Down)
            {
                zRot = Mathf.Clamp(zRot + rotateSpeed * Time.deltaTime, 110f, 250f);
            }
            guideline.eulerAngles = new Vector3(0f, 0f, zRot);
        }
    }

    public override void OnStageFinish()
    {
        base.OnStageFinish();
        isMoving = false;
        chance = 0;
    }
}
