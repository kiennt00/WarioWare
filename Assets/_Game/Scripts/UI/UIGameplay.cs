using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class UIGameplay : UICanvas
{
    [SerializeField] TextMeshProUGUI textLives, textCount;
    [SerializeField] TextMeshProUGUI textCountdown;
    [SerializeField] Transform ImageCountdown;
    private bool isCountdown;

    private void Update()
    {
        if (isCountdown)
        {
            ImageCountdown.Rotate(0f, 0f, -360f * Time.deltaTime);
        }
    }

    public void UpdateTextLives(int lives)
    {
        textLives.text = "Lives: " + lives.ToString();
    }

    public void UpdateTextCount(int count)
    {
        textCount.text = "Count: " + count.ToString();
    }

    public void UpdateCountdown(int remainingTime)
    {
        if (remainingTime > 0)
        {
            isCountdown = true;
        }
        else
        {
            isCountdown = false;
        }

        textCountdown.text = remainingTime.ToString();       
    }
}
