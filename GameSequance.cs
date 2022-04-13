using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class GameSequance : UKH.USingletoneMono<GameSequance>
//{
//    [SerializeField] 
//    private int currentEliminatedUnitCount;
//    private int currentSpawnedUnitCount;
//
//    public bool IsWaveEnd 
//    {
//        get { return SODataPreset.Instance.CurrentWaveData.waveTotalUnitCount <= currentEliminatedUnitCount; }
//    }
//
//    public int CurrentSpawnedOffenceUnitCount { get => currentSpawnedUnitCount; set => currentSpawnedUnitCount = value; }
//    public int CurrentEliminatedUnitCount { get => currentEliminatedUnitCount; set => currentEliminatedUnitCount = value; }
// 
//    private void Start()
//    {
//        CommonGameScene.Instance.GamePlayingOff();
//        //InitInstance(0/*STAGE*/, 0); //<============= HD
//        ReStartStage(true, true);
//    }
//
//    //public void InitInstance(int stageIdx, int wave)
//    //{
//    //    GameCoreUserData.Instance.SetPlayerCurrentStage(stageIdx);
//    //    CommonGameScene.Instance.CurrentStage.InitInstacne(wave);
//    //}
//    // =============================
//    //  Ŭ���̺�Ʈ �ݹ� : GameŬ���� ȣ�⸸ �����ϵ��� ����
//
//    //GameOver
//    public void ReStartStage(bool isReChargeLife, bool isClearDefenceSpawned)
//    {
//        this.gameObject.SetActive(true);
//
//        currentSpawnedUnitCount     = 0;
//        currentEliminatedUnitCount  = 0;
//
//        #region UI
//       CommonGameScene.MainUI.ClosePnlGameOver();
//       CommonGameScene.MainUI.ClosePnlGameClear();
//       CommonGameScene.MainUI.SetTextGameTime();
//       CommonGameScene.MainUI.UpdateHUDWave();
 //       #endregion
//
//        #region RESET
//        CommonGameScene.Instance.CurrentStage.StopWave();
//        CommonGameScene.Instance.CurrentStage.ResetStageValue();
//        if (isReChargeLife) CommonGameScene.Instance.CurrentStage.ReChargeLife();
//        #endregion
//
//        #region START
//        if(isClearDefenceSpawned)
//        {
//            GameCoreUserData.Instance.ResetWave();
//            CommonGameScene.MainUI.UpdateSeedCount();
//        }
//
//        int currWave = GameCoreUserData.Instance.CurrentWaveIdx;
//        CommonGameScene.Instance.CurrentStage.InitInstacne(currWave, isClearDefenceSpawned);
//        CommonGameScene.Instance.CurrentStage.StartCountDown(5, currWave);        
//        #endregion
//
//    }
//    public void ReStartStage() //���罺������ �� ó�����̺���� ����
//    {
//        this.gameObject.SetActive(true);
//
//        currentSpawnedUnitCount = 0;
//        currentEliminatedUnitCount = 0;
//
//        #region UI
//        CommonGameScene.MainUI.ClosePnlGameOver();
//        CommonGameScene.MainUI.ClosePnlGameClear();
//        CommonGameScene.MainUI.SetTextGameTime();
//        CommonGameScene.MainUI.UpdateHUDWave();
//        #endregion
//
//        #region RESET
//        CommonGameScene.Instance.CurrentStage.StopWave();
//        CommonGameScene.Instance.CurrentStage.ResetStageValue();
//        CommonGameScene.Instance.CurrentStage.ReChargeLife();
//        #endregion
//
//        #region START
//        GameCoreUserData.Instance.ResetWave();
//        CommonGameScene.MainUI.UpdateSeedCount();
//        CommonGameScene.MainUI.UpdateBuildableButtonState();
//        CommonGameScene.MainUI.UpdateHUDWave();
//        int currWave = GameCoreUserData.Instance.CurrentWaveIdx;
//        CommonGameScene.Instance.CurrentStage.InitInstacne(currWave, true);
//        CommonGameScene.Instance.CurrentStage.StartCountDown(5, currWave);
//        #endregion
//    }
//    public void ContinueStage()
//    {
//        CommonGameScene.Instance.CurrentStage.ReChargeLife();
//        CommonGameScene.Instance.CurrentStage.PauseOffContinueStage();
//        CommonGameScene.MainUI.ClosePnlGameOver();
//        this.CheckGameFinish();
//    }
//    public void GotoTitle()
//    {
//        CommonGameScene.Instance.GamePlayingOff();
//        CommonGameScene.MainUI.ClosePnlGameOver();
//        CommonGameScene.MainUI.ClosePnlGameClear();
//        USceneManager.Instance.LoadNormalScene(ESceneType.Title);
//    }
//
//    //StageClear
//    //public void GoNextStage()
//    //{
//    //    int prevStage = GameCoreUserData.Instance.CurrentStageIdx;
//    //
//    //    if (GameCoreUserData.Instance.GoToNextStage())
//    //    {
//    //        //������������ ��Ȱ��ȭ
//    //        CommonGameScene.Instance.Stages[prevStage].DeActive();
//    //
//    //        //���ο� �������� Ȱ��ȭ
//    //        this.ReStartStage(false, true);
//    //    }
//    //    else
//    //    {
//    //        Debug.LogError("���� ����");
//    //    }
//    //}
//    // ======================================================
//    //
//    //
//    // ======================================================
//
//    public void CheckGameFinish()
//    {
//        //������� ������
//        if (GameCoreUserData.Instance.CurrentLife <= 0)
//        {
//            CommonGameScene.Instance.GamePlayingOff();
//            CommonGameScene.MainUI.OpenPnlGameOver();
//            CommonGameScene.Instance.CurrentStage.PauseStage();
//        }
//        else //����� �����ִٸ�
//        {
//            if (IsWaveEnd)
//            {
//                ChangeNextWave();
//            }
//        }
//    }
//    public void ChangeNextWave()
//    {
//        this.BroadcastAllDead();
//
//        if (GameCoreUserData.Instance.GoToNextWave())
//        {
//            this.ReStartStage(false, false);
//            CommonGameScene.MainUI.UpdateHUDWave();
//        }
//        else
//        {
//            //�������� Ŭ����!!
//            CommonGameScene.Instance.GamePlayingOff();
//            CommonGameScene.MainUI.OpenPnlGameClear();
//            CommonGameScene.Instance.CurrentStage.PauseStage();
//        }
//    }
//
//    public void IncreaseUnitCountFinish(int lifeVal)
//    {
//        currentEliminatedUnitCount++;
//        GameCoreUserData.Instance.ReduceLife(lifeVal);
//        CommonGameScene.MainUI.SetTextLife();
//    }
//
//    public void IncreaseUnitCountElminated()
//    {
//        currentEliminatedUnitCount++;
//    }
//   
//    private void Update()
//    {
//        if(Input.GetKeyDown(KeyCode.F1))
//        {
//            Time.timeScale *= 2f;
//        }
//        if (Input.GetKeyDown(KeyCode.F2))
//        {
//            Time.timeScale *= 0.5f;
//        }
//    }
//
//    // ===================================
//    public void AddBuilding(BuildableUnit newBuild)
//    {
//        CommonGameScene.Instance.SpawnManagerDefence.AddBuilding(newBuild);
//    }
//    public void BroadcastDead(OffenceUnit offenceUnit)
//    {
//        CommonGameScene.Instance.SpawnManagerDefence.BroadcastDead(offenceUnit);
//    }
//    public void BroadcastAllDead()
//    {
//        CommonGameScene.Instance.SpawnManagerDefence.BroadcastAllDead();
//    }
//}
