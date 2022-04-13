using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class BUAttackable : BuildableUnit
{
    //DATA
    [SerializeField] BUATrigger trigger;
    [SerializeField] GameObject bulletRoot;
    protected BulletSystemBase[] bullets;

    protected IEnumerator coProcAttack;
    protected IEnumerator coProcTurnHead;
    protected OffenceUnit cashTarget;

    public List<OffenceUnit> targetUnitQueue;

    protected bool isAttackDoing;

    public List<OffenceUnit> TargetUnitQueue 
    { 
        get
        { 
            if(targetUnitQueue == null) targetUnitQueue = new List<OffenceUnit>();
            return targetUnitQueue;
        } 
    }   
    public float GetAttackDamage() 
    {
        return SODataPreset.Instance.CurrentUnitDefenceData(Type).attackDamage 
            + (base.Level * SODataPreset.Instance.CurrentUnitDefenceData(Type).statusAmt_damage);
    }
    public float GetBulletSpeed()
    {
        return SODataPreset.Instance.CurrentUnitDefenceData(Type).bulletSpeed
            + (base.Level * SODataPreset.Instance.CurrentUnitDefenceData(Type).statusAmt_bulletSpeed);
    }
    public float GetAttackRadius()
    {
        return SODataPreset.Instance.CurrentUnitDefenceData(Type).attackRadius
            + (base.Level * SODataPreset.Instance.CurrentUnitDefenceData(Type).statusAmt_attackRadius);
    }
    protected virtual void Start()
    {
        bullets = bulletRoot.GetComponentsInChildren<BulletSystemBase>();
        isAttackDoing = false;
        base.IsBuildOn = false;
    }

    #region Override
    public abstract void StartAttack();

    public override void InitInstance(EUnitPresetType type)
    {
        base.InitInstance(type);
    }
    public override void SimulateOn()
    {
        base.SimulateOn();
        trigger.gameObject.SetActive(true);
        trigger.GetComponent<SphereCollider>().enabled = false;
        trigger.ChageRadiusSize(this.GetAttackRadius());
    }
    public override void SimulateOff()
    {
        base.SimulateOff();
        trigger.GetComponent<SphereCollider>().enabled = false;
        trigger.gameObject.SetActive(false);
        TargetUnitQueue.Clear();
    }
    public override void BuildOn()
    {
        base.BuildOn();
        base.IsBuildOn = true;
        trigger.GetComponent<SphereCollider>().enabled = true;

        //다 공격준비
        //if (coProcAttack != null) StopCoroutine(coProcAttack);
        //coProcAttack = CoAttackUpdate();
        //StartCoroutine(coProcAttack);

        //trigger.gameObject.SetActive(true);
        //trigger.ChageRadiusSize(this.GetAttackRadius());
    }
    public override void UpgradeLevel()
    {
        base.UpgradeLevel();

        //반지름재설정
        trigger.ChageRadiusSize(this.GetAttackRadius());
    }
   
    public new void OnClickObjectProc()
    {
        base.OnClickObjectProc();
        trigger.gameObject.SetActive(true); //빌드-어택가능에서는 트리거 영역 처리
      
    }
    public new void OnClickLeave()
    {
        base.OnClickLeave();
        trigger.gameObject.SetActive(false); //빌드-어택가능에서는 트리거 영역 처리
    }
    public void AttackStart()
    {
        StartAttack();
     
    }

    protected void StartHeadTurn(OffenceUnit target)
    {
        if (coProcTurnHead != null) StopCoroutine(coProcTurnHead);
        coProcTurnHead = CoHeadTurnUpdate(target);
        StartCoroutine(coProcTurnHead);
    }

    private IEnumerator CoHeadTurnUpdate(OffenceUnit target)
    {
        float time = 0f;
        Vector3 dist = target.transform.position - transform.position;
        Quaternion startQt = transform.rotation;
        while (time <= 1.5f)
        {
            yield return null;
            float angle = Mathf.Atan2(dist.x, dist.z) * Mathf.Rad2Deg;
            Quaternion EndQt = Quaternion.AngleAxis(angle, Vector3.up);
            time += Time.deltaTime * 2;
            transform.rotation = Quaternion.Slerp(startQt, EndQt, time);//;
                                                                        //root.transform.rotation = Quaternion.Euler(0f, Mathf.Lerp(startQt.y, Endangle, time), 0f);//;
        }
    }
    #endregion

    #region event  
    public void AddTargetUnit(OffenceUnit target)
    {
        TargetUnitQueue.Add(target);
        AttackStart();
    }
    public void RemoveTargetUnit(OffenceUnit target)
    {
        //bool chker = false;
        foreach(OffenceUnit targetUnit in TargetUnitQueue)
        {
            if (targetUnit == target)
            {
                TargetUnitQueue.Remove(targetUnit);
                //chker = true;
                break;
            }
        }
        //if (chker == false) 
        //    Debug.LogError($"정상삭제안됨{target.name}를 지우라고했는데? {this.gameObject.name} 이 못하 ");
    }
    public void RemoveAllTargetUnit()
    {
        TargetUnitQueue.Clear();
    }
    #endregion
}
