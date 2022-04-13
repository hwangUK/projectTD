using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OffenceUnit : UnitBase
{
    public enum ELine { left, mid, right, max}

    [SerializeField] private int    currentTargetPont;
    [SerializeField] private ELine  currentLane;
    [SerializeField] private BoxCollider boxCollider;

    //[SerializeField] private int id;
    private IEnumerator coInit;
    private IEnumerator coTurn;
    private FootPath    currentFootPath;

    //DATA
    [Space(10)]
    [SerializeField] private float  gameHp;
    [SerializeField] private int    spawnCount;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == LayerMask.NameToLayer("bullet"))
        {
            _other.gameObject.GetComponent<Bullet>().BulletDamageToOffenceUnit(this);
        }
    }
    public new void OnClickObjectProc()
    {
        base.OnClickObjectProc();
    }

    public new void OnClickLeave()
    {
        base.OnClickLeave();
    }
    //===================================================
    #region Init
    public void InitInstance(FootPath _footPath)
    {
        base.InitInstance();
        base.SetAliveFalse();
        currentFootPath = _footPath;
        currentLane = (ELine)Random.Range(0, (int)ELine.max);
        currentTargetPont = 0;
    }
    public void ResetPos()
    {
        this.transform.position = currentFootPath.GetTargetPos(0);
    }
    public void AliveSpawnOn(int _maxHP, int _id)
    {
        base.SetAliveTrue();
        boxCollider.enabled = true;
        gameHp = _maxHP;
    }
    public void AliveSpawnOff()
    {
        base.SetAliveFalse();
        currentTargetPont = 0;
        boxCollider.enabled = false;
        CommonGameScene.Game.BroadcastDead(this);
    }
  
    #endregion
    //===================================================
    #region movemenet
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (base.IsAlive == false) return;
        if (currentFootPath == null) return;

        Vector3 targetPos = currentFootPath.GetTargetPos(currentTargetPont);

        //이동하자
        this.transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            Time.deltaTime * base.freezeSpeed);

        //완전 다 도착했는데?
        if (Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            //마지막 경로라면 종료
            if (currentTargetPont + 1 >= currentFootPath.Path.Length)//.GetLength(1))
            {
                //Debug.LogError(this.gameObject.name + " 마지막");
                AliveSpawnOff();
                CommonGameScene.Game.IncreaseUnitCountFinish(1);
                CommonGameScene.Game.CheckGameFinish();
            }
            else
            {
                //다음 경로로
                currentTargetPont++;
                Vector3 nextTargetPos = currentFootPath.GetTargetPos(currentTargetPont);

                //회전!
                if (coTurn != null) StopCoroutine(coTurn);
                coTurn = CoTurn(nextTargetPos);
                StartCoroutine(coTurn);
            }
        }
    }

    private IEnumerator CoTurn(Vector3 targetPos)
    {
        float time = 0f;
        Vector3 dist = targetPos - transform.position;
        Quaternion startQt = transform.rotation;
        while (time <= 2.0f)
        {
            yield return null;
            float angle      = Mathf.Atan2(dist.x, dist.z) * Mathf.Rad2Deg;
            Quaternion EndQt = Quaternion.AngleAxis(angle, Vector3.up);
            time += Time.deltaTime * 2;
            transform.rotation = Quaternion.Slerp(startQt, EndQt, time);//;
            //root.transform.rotation = Quaternion.Euler(0f, Mathf.Lerp(startQt.y, Endangle, time), 0f);//;
        }
    }
    #endregion

    //===================================================
    #region event
    public bool Damaged(BuildableUnit src)
    {
        if (gameHp < 0) return true;

        if(src is BUAttackable)
        {
            BUAttackable tmp = src as BUAttackable;
            gameHp -= tmp.GetAttackDamage();
            if (gameHp < 0)
            {
                Death(src);
                return true;
            }
        }
        return false;
    }
    public void Death(BuildableUnit src)
    {
        CommonGameScene.Game.IncreaseUnitCountElminated();
        CommonGameScene.Game.CheckGameFinish();
        AliveSpawnOff();
    }
    #endregion
}
