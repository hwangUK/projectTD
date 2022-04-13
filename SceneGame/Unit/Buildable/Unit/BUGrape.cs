using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUGrape : BUAttackable
{
    public override void StartAttack()
    {
        if (isAttackDoing == false)
        {
            if (coProcAttack != null) StopCoroutine(coProcAttack);
            coProcAttack = CoAttackUpdate();
            StartCoroutine(coProcAttack);
        }
    }

    private IEnumerator CoAttackUpdate()
    {
        isAttackDoing = true;

        while (TargetUnitQueue.Count > 0)
        {
            while (base.freezeSpeed == 0f) yield return new WaitForSeconds(1);

            cashTarget = TargetUnitQueue[0];
            StartHeadTurn(cashTarget);
            yield return new WaitForSeconds(0.2f); //머리돌아가는시간 나중에 앵글 계산값으로 적용하자
            if (cashTarget.IsAlive) //아직도 타겟이 유효하네
            {
                try
                {
                    foreach (var bul in bullets) //풀에서 찾기
                    {
                        if (bul.isActive == false)
                        {
                            bul.BulletAwake();
                            bul.ShotBullets(this.transform.position, this, TargetUnitQueue.ToArray(), false);
                            break;
                        }
                    }
                }
                catch
                {
                    print("AttackStart() 예외발생");
                }

                #region onemoreCheck
                //한번더 확인
                //if (TargetUnitQueue[0].IsSetActiveOn)
                //{
                //    cashTarget = TargetUnitQueue[0];
                //    StartHeadTurn(cashTarget);
                //    yield return new WaitForSeconds(0.2f); //머리돌아가는시간 나중에 앵글 계산값으로 적용하자
                //    AttackStart();
                //    break;
                //}
                //else //죽은놈이니까 지워라
                //{
                //    this.RemoveTargetUnit(TargetUnitQueue[0]);
                //}
                #endregion

                yield return new WaitForSeconds(GetDoingInterval());
            }
            else
            {
                yield return null;
            }
        }
        isAttackDoing = false;
    }
}
