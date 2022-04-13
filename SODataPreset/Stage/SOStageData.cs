using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StageData")]
public class SOStageData : ScriptableObject
{
    public int stageRechargeLife;
    public SpawnWave[] presetWave;
}

[System.Serializable]
public class SpawnWave
{
    public int waveTotalUnitCount;
    public SpawnKindPreset[] spawnKindUnit;
}

[System.Serializable]
public class SpawnKindPreset
{
    public int   hp;
    public float startTime;
    public float endTime;
    public float delayTime;
    public EUnitPresetType unitType;
}
