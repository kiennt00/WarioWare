using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStage : Stage
{
    protected override void OnPointerDownActionHandle(PressButtonType type)
    {
        Debug.Log("Pointer down " + type);
    }

    protected override void OnPressActionHandle(PressButtonType type)
    {
        Debug.Log("Press " + type);
    }

    protected override void OnPointerUpActionHandle(PressButtonType type)
    {
        Debug.Log("Pointer Up " + type);
    }
}
