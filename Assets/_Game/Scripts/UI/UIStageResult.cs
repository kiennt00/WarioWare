using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStageResult : UICanvas
{
    [SerializeField] TextMeshProUGUI textResult;

    public void UpdateTextResult(string text)
    {
        textResult.text = text;
    }
}
