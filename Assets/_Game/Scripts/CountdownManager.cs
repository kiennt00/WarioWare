using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownManager : Singleton<CountdownManager>
{
    public void StartCountdown(int totalSeconds, Action<int> onTick, Action onComplete)
    {
        StartCoroutine(CountdownCoroutine(totalSeconds, onTick, onComplete));
    }

    private IEnumerator CountdownCoroutine(int totalSeconds, Action<int> onTick, Action onComplete)
    {
        int remainingTime = totalSeconds;

        while (remainingTime > 0)
        {
            onTick?.Invoke(remainingTime);
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        onTick?.Invoke(remainingTime);
        onComplete?.Invoke();
    }
}
