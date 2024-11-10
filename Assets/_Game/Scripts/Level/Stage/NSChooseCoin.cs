using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class NSChooseCoin : NormalStage
{
    [SerializeField] Transform coin;
    [SerializeField] Transform choosing;
    [SerializeField] List<Transform> listCup;
    [SerializeField] List<Transform> listSprite;
    [SerializeField] float height = 2f;
    [SerializeField] float duration = 0.5f;

    private Transform coinParent;
    private int choosingIndex;

    private int currentSwapTime = 0;
    private List<(int, int)> listCoupleIndexSwap = new();

    private int chance = 1;
    private bool isPlaying = false;

    public override void InitStage()
    {
        base.InitStage();

        int index = UnityEngine.Random.Range(0, listCup.Count);
        coinParent = listCup[index];
        coin.SetParent(listCup[index]);
        coin.localPosition = Vector3.zero;

        StartCoroutine(IEStartStage());
    }

    IEnumerator IEStartStage()
    {
        yield return MyConstants.WFS_1_S;
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < listSprite.Count; i++)
        {
            sequence.Join(listSprite[i].DOLocalMoveY(0, duration).SetEase(Ease.Linear));
        }
        sequence.OnComplete(() =>
        {
            InitSwap();
        });
        sequence.Play();
    }

    private void InitSwap()
    {
        int swapTime = UnityEngine.Random.Range(4, 6);

        for (int i = 0; i < swapTime; i++)
        {
            int index1;
            int index2;

            index1 = UnityEngine.Random.Range(0, listCup.Count);
            do
            {
                index2 = UnityEngine.Random.Range(0, listCup.Count);
            }
            while (index1 == index2);

            listCoupleIndexSwap.Add((index1, index2));
        }

        StartNextSwap();
    }

    private void StartNextSwap()
    {
        if (currentSwapTime >= listCoupleIndexSwap.Count)
        {
            choosing.gameObject.SetActive(true);
            choosing.SetParent(listCup[0]);
            choosing.localPosition = Vector3.zero;
            choosingIndex = 0;
            isPlaying = true;
            return;
        }

        var (index1, index2) = listCoupleIndexSwap[currentSwapTime];
        Transform tf1 = listCup[index1];
        Transform tf2 = listCup[index2];

        // Swap couple in listCup and listSprite
        (listCup[index2], listCup[index1]) = (listCup[index1], listCup[index2]);
        (listSprite[index2], listSprite[index1]) = (listSprite[index1], listSprite[index2]);

        void onComplete()
        {
            currentSwapTime++;
            StartNextSwap();
        }

        SwapPosition(tf1, tf2, onComplete);
    }

    private void SwapPosition(Transform tf1, Transform tf2, Action onComplete)
    {
        Vector3 middlePoint = (tf1.position + tf2.position) / 2;
        Vector3 controlPoint1 = middlePoint + Vector3.up * height;
        Vector3 controlPoint2 = middlePoint - Vector3.up * height;

        Vector3[] path1 = new Vector3[] { tf1.position, controlPoint1, tf2.position };
        Vector3[] path2 = new Vector3[] { tf2.position, controlPoint2, tf1.position };

        Sequence sequence = DOTween.Sequence();

        sequence.Join(tf1.DOPath(path1, duration, PathType.CatmullRom).SetEase(Ease.Linear))
            .Join(tf2.DOPath(path2, duration, PathType.CatmullRom).SetEase(Ease.Linear))
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        sequence.Play();
    }

    protected override void OnPointerDownActionHandle(PressButtonType type)
    {
        if (isPlaying && chance > 0)
        {
            if (type == PressButtonType.Left)
            {
                if (choosingIndex <= 0) return;
                choosingIndex--;
                choosing.SetParent(listCup[choosingIndex]);
                choosing.localPosition = Vector3.zero;
            }
            else if (type == PressButtonType.Right)
            {
                if (choosingIndex >= listCup.Count - 1) return;
                choosingIndex++;
                choosing.SetParent(listCup[choosingIndex]);
                choosing.localPosition = Vector3.zero;
            }
            else if (type == PressButtonType.A)
            {
                chance--;
                isPlaying = false;
                listSprite[choosingIndex].DOLocalMoveY(1, duration).SetEase(Ease.Linear);

                if (listCup[choosingIndex] == coinParent)
                {
                    isStageWin = true;
                }
                else
                {
                    int index = listCup.IndexOf(coinParent);
                    listSprite[index].DOLocalMoveY(1, duration).SetEase(Ease.Linear);
                }
            }
        }
    }

    public override void OnStageFinish()
    {
        base.OnStageFinish();
        isPlaying = false;
        chance = 0;
    }
}
