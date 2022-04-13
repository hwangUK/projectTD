using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EBuildableUnitState
{
    none, simulate, onGame
}
public abstract class BuildableUnit : UnitBase
{
    [SerializeField] private EUnitPresetType type;
    [SerializeField] private int level;
    public EBuildableUnitState buildState;
    private SOUnitDefenceData originData;
    private bool isBuildOn;
    public SOUnitDefenceData OriginData { get => SODataPreset.Instance.CurrentUnitDefenceData(Type); }
    public int NeedSeedCount { get => SODataPreset.Instance.CurrentUnitDefenceData(Type).needSeedCount; }
    public int GetUpgradeNeedSeedCount 
    { 
        get => SODataPreset.Instance.CurrentUnitDefenceData(Type).needSeedCount +
            (level * SODataPreset.Instance.CurrentUnitDefenceData(Type).statusAmt_needSeed); 
    }
    public float GetDoingInterval()
    {
        return Mathf.Clamp(
            SODataPreset.Instance.CurrentUnitDefenceData(Type).doingIntervalTime - (level * SODataPreset.Instance.CurrentUnitDefenceData(Type).statusAmt_doingInterval),
            0f,
            SODataPreset.Instance.CurrentUnitDefenceData(Type).doingIntervalTime);
    }
    public EUnitPresetType Type { get => type; }
    public int Level { get => level;}
    public bool IsBuildOn { get => isBuildOn; set => isBuildOn = value; }

    public new void OnClickObjectProc()
    {
        base.OnClickObjectProc();
        CommonGameScene.MainUI.OpenPannelUnitInfo(this); // 버튼 입력과 같은 기능을 버튼 프로시저에서 하는중
    }

    public new void OnClickLeave()
    {
        base.OnClickLeave();
    }

    public virtual void UpgradeLevel() 
    { 
        level++; 
    }

    public virtual void InitInstance(EUnitPresetType _type)
    {
        base.InitInstance();
        base.SetAliveFalse();
        type = _type;
        level = 0;
        buildState = EBuildableUnitState.none;
    }

    public virtual void SimulateOn()
    {
        base.SetAliveTrue();
        buildState = EBuildableUnitState.simulate;
        //this.transform.position = Vector3.back * 50f; //화면 밖으로
    }

    public virtual void SimulateOff()
    {
        buildState = EBuildableUnitState.none;
        base.SetAliveFalse();
    }

    public virtual void BuildOn()
    {
        buildState = EBuildableUnitState.onGame;
        GameCoreUserData.Instance.ReduceSeedCount(NeedSeedCount);
        CommonGameScene.MainUI.UpdateSeedCount();
        CommonGameScene.MainUI.UpdateBuildableButtonState();

        CommonGameScene.Game.AddBuilding(this);
    }
}
