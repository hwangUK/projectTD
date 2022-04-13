using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] FootPath footPath;
    public FootPath FootPath { get => footPath; set => footPath = value; }

    private bool    onCountDown;
    public float    waveElapsedTime;
    public float    currentCountdownTime;

    private float   waveGlobalTimeSpeed;
    private int     countDownTime;
    private int     currentWaveCount;

    //REF =========================================
    private SpawnManagerOffence spawnManagerOffence;
    private SpawnManagerDefence spawnManagerDefence;

    [SerializeField]
    private int currentEliminatedUnitCount;
    private int currentSpawnedUnitCount;

    private void Start()
    {
        CommonGameScene.Instance.LinkCurrentStage(this); 
        CommonGameScene.Instance.GamePlayingOff();
        //InitInstance(0/*STAGE*/, 0); //<============= HD
        ReStartStage(true, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale *= 2f;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Time.timeScale *= 0.5f;
        }
        if (onCountDown)
        {
            currentCountdownTime += Time.deltaTime;
            CommonGameScene.MainUI.SetTextCountdown((int)(countDownTime - currentCountdownTime));
            if (currentCountdownTime > countDownTime)
            {
                this.StopCountDown();
                CommonGameScene.MainUI.SetTextCountdown();
                this.StartNewWave();
            }
        }
        if (CommonGameScene.Instance.IsGamePlaying)
        {
            waveElapsedTime += (Time.deltaTime * waveGlobalTimeSpeed);
            CommonGameScene.MainUI.SetTextGameTime(waveElapsedTime);
        }
    }
    public void InitInstacne(int wave, bool isClearDefenceSpawned)
    {
        this.gameObject.SetActive(true);

        spawnManagerOffence = CommonGameScene.Instance.SpawnManagerOffence;
        spawnManagerDefence = isClearDefenceSpawned ? CommonGameScene.Instance.SpawnManagerDefence : spawnManagerDefence;
        onCountDown = false;
        waveGlobalTimeSpeed = 1f;
        waveElapsedTime = 0f;
        currentCountdownTime = 0f;
        countDownTime = 0;
        currentWaveCount = 0;

        footPath.InitInstance();
        spawnManagerOffence.InitInstance(wave);

        if (isClearDefenceSpawned)
        {
            spawnManagerDefence.InitInstance();
        }
    }

    public bool IsWaveEnd
    {
        get { return SODataPreset.Instance.CurrentWaveData.waveTotalUnitCount <= currentEliminatedUnitCount; }
    }
    public int CurrentSpawnedOffenceUnitCount { get => currentSpawnedUnitCount; set => currentSpawnedUnitCount = value; }
    public int CurrentEliminatedUnitCount { get => currentEliminatedUnitCount; set => currentEliminatedUnitCount = value; }

    // ===================================
    //                                   =
    //                                   =
    //                                   =
    //                                   =
    // ===================================

    public void ReStartStage(bool isReChargeLife, bool isClearDefenceSpawned)
    {
        this.gameObject.SetActive(true);

        currentSpawnedUnitCount = 0;
        currentEliminatedUnitCount = 0;

        #region UI
        CommonGameScene.MainUI.ClosePnlGameOver();
        CommonGameScene.MainUI.ClosePnlGameClear();
        CommonGameScene.MainUI.SetTextGameTime();
        CommonGameScene.MainUI.UpdateHUDWave();
        #endregion

        #region RESET
        this.StopWave();
        this.ResetStageValue();
        if (isReChargeLife) this.ReChargeLife();
        #endregion

        #region START
        if (isClearDefenceSpawned)
        {
            GameCoreUserData.Instance.ResetWave();
            CommonGameScene.MainUI.UpdateSeedCount();
        }

        int currWave = GameCoreUserData.Instance.CurrentWaveIdx;
        this.InitInstacne(currWave, isClearDefenceSpawned);
        this.StartCountDown(5, currWave);
        #endregion

    }
    public void ReStartStage() //���罺������ �� ó�����̺���� ����
    {
        this.gameObject.SetActive(true);

        currentSpawnedUnitCount = 0;
        currentEliminatedUnitCount = 0;

        #region UI
        CommonGameScene.MainUI.ClosePnlGameOver();
        CommonGameScene.MainUI.ClosePnlGameClear();
        CommonGameScene.MainUI.SetTextGameTime();
        CommonGameScene.MainUI.UpdateHUDWave();
        #endregion

        #region RESET
        this.StopWave();
        this.ResetStageValue();
        this.ReChargeLife();
        #endregion

        #region START
        GameCoreUserData.Instance.ResetWave();
        CommonGameScene.MainUI.UpdateSeedCount();
        CommonGameScene.MainUI.UpdateBuildableButtonState();
        CommonGameScene.MainUI.UpdateHUDWave();
        int currWave = GameCoreUserData.Instance.CurrentWaveIdx;
        this.InitInstacne(currWave, true);
        this.StartCountDown(5, currWave);
        #endregion
    }
    public void ContinueStage()
    {
        this.ReChargeLife();
        this.PauseOffContinueStage();
        this.CheckGameFinish();
        CommonGameScene.MainUI.ClosePnlGameOver();
    }
    public void GotoTitle()
    {
        CommonGameScene.Instance.GamePlayingOff();
        CommonGameScene.MainUI.ClosePnlGameOver();
        CommonGameScene.MainUI.ClosePnlGameClear();
        USceneManager.Instance.LoadNormalScene(ESceneType.Title);
    }

    public void CheckGameFinish()
    {
        //������� ������
        if (GameCoreUserData.Instance.CurrentLife <= 0)
        {
            CommonGameScene.Instance.GamePlayingOff();
            CommonGameScene.MainUI.OpenPnlGameOver();
            this.PauseStage();
        }
        else //����� �����ִٸ�
        {
            if (IsWaveEnd)
            {
                ChangeNextWave();
            }
        }
    }
    public void ChangeNextWave()
    {
        this.BroadcastAllDead();

        if (GameCoreUserData.Instance.GoToNextWave())
        {
            this.ReStartStage(false, false);
            CommonGameScene.MainUI.UpdateHUDWave();
        }
        else
        {
            //�������� Ŭ����!!
            CommonGameScene.Instance.GamePlayingOff();
            CommonGameScene.MainUI.OpenPnlGameClear();
            this.PauseStage();
        }
    }

    public void IncreaseUnitCountFinish(int lifeVal)
    {
        currentEliminatedUnitCount++;
        GameCoreUserData.Instance.ReduceLife(lifeVal);
        CommonGameScene.MainUI.SetTextLife();
    }
    public void IncreaseUnitCountElminated()
    {
        currentEliminatedUnitCount++;
    }

    // ===================================
    //                                   =
    //                                   =
    //                                   =
    //                                   =
    // ===================================

    public void AddBuilding(BuildableUnit newBuild)
    {
        CommonGameScene.Instance.SpawnManagerDefence.AddBuilding(newBuild);
    }
    public void BroadcastDead(OffenceUnit offenceUnit)
    {
        CommonGameScene.Instance.SpawnManagerDefence.BroadcastDead(offenceUnit);
    }
    public void BroadcastAllDead()
    {
        CommonGameScene.Instance.SpawnManagerDefence.BroadcastAllDead();
    }
   
    public void DeActive()
    { 
        this.gameObject.SetActive(false);
    }

    public void StartCountDown(int countTime, int wavePoint)
    {
        countDownTime       = countTime;
        currentWaveCount    = wavePoint;
        //ī��Ʈ�ٿ����
        onCountDown = true;
        currentCountdownTime = 0f;
        waveElapsedTime = 0f;
    }
    public void StopCountDown()
    {
        //ī��Ʈ�ٿ����
        onCountDown = false;
    }
    public void StartNewWave()
    {
        if (spawnManagerOffence == null) return;

        //���̺� ����
        CommonGameScene.Instance.GamePlayingOn();
        waveElapsedTime = 0f;
        spawnManagerOffence.SpawnStart(currentWaveCount);
    }
    public void StopWave()
    {
        if(spawnManagerOffence == null) return;

        CommonGameScene.Instance.GamePlayingOff();
        spawnManagerOffence.SpawnEnd();
    }

    //===================================
    public void PauseStage()
    {
        if (spawnManagerOffence == null) return;

        waveGlobalTimeSpeed = 0f; //GTime ����
        spawnManagerOffence.SetFreezeOn(); //����      
        spawnManagerDefence.SetFreezeOn();
    }
    public void PauseOffContinueStage()
    {
        if (spawnManagerOffence == null) return;

        CommonGameScene.Instance.GamePlayingOn();
        waveGlobalTimeSpeed = 1f; //GTime ����
        spawnManagerOffence.SetFreezeOff();        //�ٽ� ����       
        spawnManagerDefence.SetFreezeOff();
    }
    public void ResetStageValue()
    {
        if (spawnManagerOffence == null) return;

        waveGlobalTimeSpeed = 1f; //GTime ����
        spawnManagerOffence.SetFreezeOff();
        spawnManagerOffence.AliveOffAll();
    }

    //===================================

    public void StageClear()
    {

    }
    public void ReChargeLife()
    {
        int rechargeLife = SODataPreset.Instance.CurrentStageData.stageRechargeLife;
        GameCoreUserData.Instance.RechargeLife(rechargeLife);
        CommonGameScene.MainUI.SetTextLife();
    } 
}

//StageClear
//public void GoNextStage()
//{
//    int prevStage = GameCoreUserData.Instance.CurrentStageIdx;
//
//    if (GameCoreUserData.Instance.GoToNextStage())
//    {
//        //������������ ��Ȱ��ȭ
//        CommonGameScene.Instance.Stages[prevStage].DeActive();
//
//        //���ο� �������� Ȱ��ȭ
//        this.ReStartStage(false, true);
//    }
//    else
//    {
//        Debug.LogError("���� ����");
//    }
//}
// ======================================================
//
//
// ======================================================
