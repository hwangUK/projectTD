using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUATrigger : UKH.UCustomMono<BUAttackable>
{
    public void ChageRadiusSize(float rdus)
    {
        this.transform.localScale = new Vector3(rdus, rdus, rdus);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("unitSelectMeshOffence") &&
            other.gameObject.GetComponent<OffenceUnit>() != null)
        {
            //Debug.Log($"{RootCore.name} 가 {other.name} 를 추가");
            RootCore.AddTargetUnit(other.transform.GetComponent<OffenceUnit>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("unitSelectMeshOffence") &&
            other.gameObject.GetComponent<OffenceUnit>() != null)
        {
            //Debug.Log($"{RootCore.name} 가 {other.name} 를 제거");
            RootCore.RemoveTargetUnit(other.transform.GetComponent<OffenceUnit>());
        }
    }

}

//if(root.GetComponent<BUPear>() != null)
//{
//Debug.Log($"{root.name} 가 {other.name} 를 제거");
//}

//CommonGameScene.Instance.SpawnManagerOffence.cashTargetingUnits.Remove(other.gameObject.GetComponent<OffenceUnit>());
//TargetUnitQueue.Remove(other.gameObject.GetComponent<OffenceUnit>());