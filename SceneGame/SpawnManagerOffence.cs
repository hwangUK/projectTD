using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerOffence : MonoBehaviour
{
    [SerializeField] 
    private GameObject[]                    unitTypes;
    private List<List<OffenceUnit>>         unitPool;
    private List<IEnumerator>               _coSpawns;

    //public List<OffenceUnit>                cashTargetingUnits;

    public void InitInstance(int wave)
    {
        //cashTargetingUnits = new List<OffenceUnit>();

        unitPool = new List<List<OffenceUnit>>();
        for(int i = 0; i < unitTypes.Length; i++)
        {
            unitPool.Add(new List<OffenceUnit>(unitTypes[i].GetComponentsInChildren<OffenceUnit>(true)));
        }

        for (int i = 0; i < unitPool.Count; i++)
        {
            for (int j = 0; j < unitPool[i].Count; j++)
            {
                unitPool[i][j].InitInstance(CommonGameScene.Game.FootPath);
            }
        }

        InitCoSpawn(wave);
        AliveOffAll();
    }
    public void InitCoSpawn(int wave)
    {
        _coSpawns = new List<IEnumerator>();
        SOStageData stageData = SODataPreset.Instance.StageData[GameCoreUserData.Instance.CurrentStageIdx];
        foreach (SpawnKindPreset preset in stageData.presetWave[wave].spawnKindUnit)
        {
            _coSpawns.Add(
                CoSpawn(preset.hp,
                        preset.startTime,
                        preset.endTime,
                        preset.delayTime,
                        preset.unitType,
                        stageData.presetWave[wave].waveTotalUnitCount
                        ));
        }
    }
    public void AliveOffAll()
    {
        for (int i = 0; i < unitPool.Count; i++)
        {
            for (int j = 0; j < unitPool[i].Count; j++)
            {
                unitPool[i][j].AliveSpawnOff();
            }
        }
    }

    public void SetFreezeOn()
    {
        for (int i = 0; i < unitPool.Count; i++)
        {
            for (int j = 0; j < unitPool[i].Count; j++)
            {
                unitPool[i][j].SetSpeedFreeze(); //멈춤
            }
        }
    }
    public void SetFreezeOff()
    {
        for (int i = 0; i < unitPool.Count; i++)
        {
            for (int j = 0; j < unitPool[i].Count; j++)
            {
                unitPool[i][j].SetSpeedOrigin(); //원래속도로
            }
        }
    }
    public void SpawnStart(int wave)
    {
        InitCoSpawn(wave);

        foreach (IEnumerator item in _coSpawns)
        {
            StartCoroutine(item);
        }
    } 

    public void SpawnEnd()
    {
        foreach (IEnumerator item in _coSpawns)
        {
            if(item != null)
                StopCoroutine(item);
        }
    }

    IEnumerator CoSpawn(int maxHP, float startTime, float endTime, float delaytime, EUnitPresetType uType, int count)
    {
        float currentTime  = startTime;

        while(CommonGameScene.Game.CurrentSpawnedOffenceUnitCount < count)
        {
            bool canSpawn = false;

            if(CommonGameScene.Game.waveElapsedTime > currentTime)
            {
                if(endTime > startTime) 
                {
                    canSpawn = CommonGameScene.Game.waveElapsedTime < endTime ? true : false;
                }
                else
                {
                    canSpawn = true;
                }

                if(canSpawn)
                {
                    OffenceUnit newUnit = UKH.UUtil.FindUseableUnit<OffenceUnit>(unitPool, uType);
                    //if(newUnit.Id != -1)
                    //{
                    //    Debug.LogError("스폰한답시고가져왔는데 아이디는 아직 초기화가 안됬네 이건 생각보다 심각한 상황인데?");
                    //}
                    if (newUnit != null)
                    {
                        newUnit.ResetPos();

                        int unitID = ++CommonGameScene.Game.CurrentSpawnedOffenceUnitCount;
                        newUnit.AliveSpawnOn(maxHP, unitID);
                        currentTime += delaytime;
                    }
                    else
                    {
                        Debug.LogError("재활용가능한 OffenceUnit가 없다");
                    }
                }
            }
            yield return null;
        }
    }
}
