using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SODataPreset : UKH.USingletoneMono<SODataPreset>
{
    [SerializeField] private List<SOStageData> stageData;
    [SerializeField] private List<SOUnitDefenceData> unitDefenceData;

    public List<SOUnitDefenceData>  UnitDefenceData { get => unitDefenceData; set => unitDefenceData = value; }
    public List<SOStageData>        StageData { get => stageData; set => stageData = value; }

    public SOStageData CurrentStageData
    {
        get 
        {
            return stageData[GameCoreUserData.Instance.CurrentStageIdx];
        }
    }
    public SpawnWave CurrentWaveData
    {
        get
        {
            return stageData[GameCoreUserData.Instance.CurrentStageIdx].presetWave[GameCoreUserData.Instance.CurrentWaveIdx];
        }
    }
    public SOUnitDefenceData CurrentUnitDefenceData(EUnitPresetType type)
    {
        return unitDefenceData.Find(x => x.type == type);
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
