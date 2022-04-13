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
            yield return new WaitForSeconds(0.2f); //�Ӹ����ư��½ð� ���߿� �ޱ� ��갪���� ��������
            if (cashTarget.IsAlive) //������ Ÿ���� ��ȿ�ϳ�
            {
                try
                {
                    foreach (var bul in bullets) //Ǯ���� ã��
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
                    print("AttackStart() ���ܹ߻�");
                }

                #region onemoreCheck
                //�ѹ��� Ȯ��
                //if (TargetUnitQueue[0].IsSetActiveOn)
                //{
                //    cashTarget = TargetUnitQueue[0];
                //    StartHeadTurn(cashTarget);
                //    yield return new WaitForSeconds(0.2f); //�Ӹ����ư��½ð� ���߿� �ޱ� ��갪���� ��������
                //    AttackStart();
                //    break;
                //}
                //else //�������̴ϱ� ������
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
