using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerDefence : MonoBehaviour
{
    [SerializeField] private GameObject[] unitTypes;
    private List<List<BuildableUnit>> unitTypePool;
    private List<BuildableUnit> allBuild;

    private bool isSimulateOn;
    private bool isBuildableArea;
    private BuildableUnit cashSelectedSimulate;

    public void InitInstance()
    {
        isSimulateOn = false;
        isBuildableArea = false;
        unitTypePool = new List<List<BuildableUnit>>(); 
        allBuild = new List<BuildableUnit>();
        for (int i = 0; i < unitTypes.Length; i++)
        {
            unitTypePool.Add(new List<BuildableUnit>(unitTypes[i].GetComponentsInChildren<BuildableUnit>(true)));
        }
        for (int i = 0; i < unitTypePool.Count; i++)
        {
            for (int j = 0; j < unitTypePool[i].Count; j++)
            {
                unitTypePool[i][j].InitInstance((EUnitPresetType)i);
            }
        }
        ActiveOffAll();
    }
    public void ActiveOffAll()
    {
        for (int i = 0; i < unitTypePool.Count; i++)
        {
            for (int j = 0; j < unitTypePool[i].Count; j++)
            {
                unitTypePool[i][j].SimulateOff();
            }
        }
    }
    public void UUpdate()
    {
        UpdateSimulate();

        if (Input.GetKeyDown(KeyCode.Q)) StartSimulate(EUnitPresetType.T0);
        if (Input.GetKeyDown(KeyCode.W)) StartSimulate(EUnitPresetType.T1);
        if (Input.GetKeyDown(KeyCode.E)) StartSimulate(EUnitPresetType.T2);
        if (Input.GetKeyDown(KeyCode.R)) StartSimulate(EUnitPresetType.T3);
        if (Input.GetKeyDown(KeyCode.T)) StartSimulate(EUnitPresetType.T4);
        if (Input.GetKeyDown(KeyCode.Y)) StartSimulate(EUnitPresetType.T5);
        if (Input.GetKeyDown(KeyCode.U)) StartSimulate(EUnitPresetType.T6);
        if (Input.GetKeyDown(KeyCode.I)) StartSimulate(EUnitPresetType.T7);
        if (Input.GetKeyDown(KeyCode.O)) StartSimulate(EUnitPresetType.T8);
        if (Input.GetKeyDown(KeyCode.P)) StartSimulate(EUnitPresetType.T9);
        if (Input.GetKeyDown(KeyCode.L)) StartSimulate(EUnitPresetType.T10);
        if (Input.GetKeyDown(KeyCode.K)) StartSimulate(EUnitPresetType.T11);
        if (Input.GetKeyDown(KeyCode.J)) StartSimulate(EUnitPresetType.T12);
        if (Input.GetKeyDown(KeyCode.H)) StartSimulate(EUnitPresetType.T13);
        if (Input.GetKeyDown(KeyCode.G)) StartSimulate(EUnitPresetType.T14);
        if (Input.GetKeyDown(KeyCode.F)) StartSimulate(EUnitPresetType.T15);

        if (Input.GetMouseButtonDown(0)) StartBuild();
    }

    public void StartSimulate(EUnitPresetType type)
    {
        isSimulateOn = true;
        cashSelectedSimulate = UKH.UUtil.FindUseableUnit<BuildableUnit>(unitTypePool, type);

        if (cashSelectedSimulate != null)
        {
            cashSelectedSimulate.SimulateOn();
            cashSelectedSimulate.OnClickLeave();
        }
    }
    public void EndSimulate()
    {
        isSimulateOn = false;
        if(cashSelectedSimulate != null)
        {
            cashSelectedSimulate.SimulateOff();
            cashSelectedSimulate = null;
        }
    }

    Ray ray;
    RaycastHit hit;
    public void UpdateSimulate()
    {
        if (isSimulateOn == false) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("buildableCell"))) //건설가능
        {
            cashSelectedSimulate.transform.position = hit.collider.transform.position;
            isBuildableArea = true;
        }
        else
        {
            isBuildableArea = false;
        }
    }
    public void StartBuild()
    {
        if (isSimulateOn && isBuildableArea)
        {
            isSimulateOn = false;
            cashSelectedSimulate.BuildOn();
        }
    }
    public void CancleSimulate()
    {
        if (isSimulateOn)
        {
            isSimulateOn = false;
            this.EndSimulate();
        }
    }

    // ==============
    public void AddBuilding(BuildableUnit newBuild)
    {
        allBuild.Add(newBuild);
    }
    public void BroadcastDead(OffenceUnit offenceUnit)
    {
        if(allBuild == null) return;

        foreach (var unit in allBuild)
        {
            if (unit is BUAttackable)
            {
                BUAttackable tmp = unit as BUAttackable;
                tmp.RemoveTargetUnit(offenceUnit);
            }
        }
    }
    public void BroadcastAllDead()
    {
        foreach (var unit in allBuild)
        {
            if (unit is BUAttackable)
            {
                BUAttackable tmp = unit as BUAttackable;
                tmp.RemoveAllTargetUnit();
            }
        }
    }
    public void SetFreezeOn()
    {
        foreach(var unit in allBuild)
        {
            unit.SetSpeedFreeze();
        }
    }
    public void SetFreezeOff()
    {
        foreach (var unit in allBuild)
        {
            unit.SetSpeedOrigin();
        }
    }
}
 

//Vector3 pos = Input.mousePosition;
//float dist = Vector3.Dot(cashSelectedSimulate.transform.position - CommonGameScene.Instance.MainCam.transform.position, 
//    CommonGameScene.Instance.MainCam.transform.forward);
//pos.z = dist;
//pos = CommonGameScene.Instance.MainCam.ScreenToWorldPoint(pos);
//cashSelectedSimulate.transform.position = new Vector3(pos.x, pos.y, pos.z);