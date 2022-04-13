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
            //Debug.Log($"{RootCore.name} �� {other.name} �� �߰�");
            RootCore.AddTargetUnit(other.transform.GetComponent<OffenceUnit>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("unitSelectMeshOffence") &&
            other.gameObject.GetComponent<OffenceUnit>() != null)
        {
            //Debug.Log($"{RootCore.name} �� {other.name} �� ����");
            RootCore.RemoveTargetUnit(other.transform.GetComponent<OffenceUnit>());
        }
    }

}

//if(root.GetComponent<BUPear>() != null)
//{
//Debug.Log($"{root.name} �� {other.name} �� ����");
//}

//CommonGameScene.Instance.SpawnManagerOffence.cashTargetingUnits.Remove(other.gameObject.GetComponent<OffenceUnit>());
//TargetUnitQueue.Remove(other.gameObject.GetComponent<OffenceUnit>());