using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoreUserData : UKH.USingletoneMonoDontDestroy<GameCoreUserData>
{
    // 플레이어 정보
    public int playermaxLife;
    public int playermaxOpendStage;
    public int playermaxOpendWave;

    // 게임 정보
    [SerializeField] private int currentLife;
    [SerializeField] private int currentStageIdx;

    //인게임 정보
    [SerializeField] private int currentWaveIdx;
    [SerializeField] private int currentSeedCount;

    public int CurrentStageIdx { get => currentStageIdx; }
    public int CurrentSeedCount { get => currentSeedCount; }
    public int CurrentWaveIdx   { get => currentWaveIdx; }
    public int CurrentLife      { get => currentLife; }

    public void ReduceSeedCount(int count) { currentSeedCount = Mathf.Clamp(currentSeedCount - count,0, currentSeedCount); }
    public void ResetWave() { currentSeedCount = 8; currentWaveIdx = 0; }
    public void RechargeLife(int life) { currentLife = Mathf.Clamp(currentLife + life, 0, playermaxLife); }
    public void ReduceLife(int life) { currentLife = Mathf.Clamp(currentLife - life, 0, playermaxLife); }
    public void IncreaseSeed() { currentSeedCount++; }

    protected override void Awake()
    {
        base.Awake();
        LoadBinData();
        ResetGameValue(0);
    }
    private void LoadBinData()
    {
        //playermaxLife = 0;
        //playermaxOpendStage = 0;
        //playermaxOpendWave = 0;
    }

    public void ResetGameValue(int currStageIdx)
    {
        currentLife     = playermaxLife /*스테이지값*/;
        currentStageIdx = currStageIdx;
        currentWaveIdx = 0;
        currentSeedCount = 9;
    }

  
    public bool GoToNextStage()
    {
        if(currentStageIdx + 1 < SODataPreset.Instance.StageData.Count)
        {
            currentStageIdx++;

            if (currentStageIdx > playermaxOpendStage)
                playermaxOpendStage = currentStageIdx;

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GoToNextWave()
    {
        if (currentWaveIdx + 1 < SODataPreset.Instance.StageData[currentStageIdx].presetWave.Length)
        {
            currentWaveIdx++;

            if (currentWaveIdx > playermaxOpendWave)
                playermaxOpendWave = currentWaveIdx;

            return true;
        }
        else
        {
            return false;
        }
    }

}
