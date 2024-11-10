using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int lives = 4;

    [SerializeField] List<NormalStage> listNormalStage = new();
    [SerializeField] BossStage bossStage;
    private Stage currentStage;
    private int currentStageIndex = 0;

    private void Awake()
    {
        this.RegisterListener(EventID.OnStageFinish, (param) =>
        {
            StartCoroutine(IEFinishStage((bool)param));
        });

        InitLevel();
    }

    public void InitLevel()
    {
        lives = 4;
        currentStageIndex = 0;
        UIManager.Ins.GetUI<UIGameplay>().UpdateTextLives(lives);
        UIManager.Ins.GetUI<UIGameplay>().UpdateTextCount(currentStageIndex);
        LoadStage(currentStageIndex);        
    }

    private void LoadStage(int stageIndex)
    {
        if (currentStage != null)
        {
            Destroy(currentStage.gameObject);
        }

        currentStageIndex = stageIndex;

        if (currentStageIndex < listNormalStage.Count)
        {
            currentStage = Instantiate(listNormalStage[currentStageIndex], this.transform);
        }
        else
        {
            currentStage = Instantiate(bossStage, this.transform);
            currentStageIndex = listNormalStage.Count;
        }

        currentStage.InitStage();
    }

    IEnumerator IEFinishStage(bool isStageWin)
    {
        UIManager.Ins.GetUI<UIStageResult>().UpdateTextResult(isStageWin ? "Stage Win" : "Stage Lose");
        UIManager.Ins.OpenUI<UIStageResult>();
        // cutscene
        yield return MyConstants.WFS_3_S;

        UIManager.Ins.CloseUI<UIStageResult>();

        FinishStage(isStageWin);
    }

    private void FinishStage(bool isStageWin)
    {
        if (isStageWin)
        {
            if (currentStage is NormalStage)
            {
                NextStage();
            }
            else if (currentStage is BossStage)
            {
                Victory();
            }
        }
        else
        {
            lives--;
            UIManager.Ins.GetUI<UIGameplay>().UpdateTextLives(lives);

            if (lives > 0)
            {
                NextStage();
            }
            else
            {
                Defeat();
            }
        }
    }

    private void NextStage()
    {
        currentStageIndex++;
        LoadStage(currentStageIndex);
        UIManager.Ins.GetUI<UIGameplay>().UpdateTextCount(currentStageIndex);
    }

    private void Victory()
    {
        Debug.Log("Victory");
    }

    private void Defeat()
    {
        Debug.Log("Defeat");
    }
}