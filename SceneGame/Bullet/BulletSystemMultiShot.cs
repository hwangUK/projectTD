using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystemMultiShot : BulletSystemBase
{
    List<OffenceUnit> targetList;
    int maxBulletCount;

    public override void ProcMove()
    {
        if (base.bullets == null) return;
        if (targetList == null) return;

        for (int i = 0; i < targetList.Count; i++)
        {
            if (i >= maxBulletCount)
                continue;

            Bullet cash = base.bullets[i];
            if (cash.IsMove)
            {
                float bulletSpeed = origin.GetBulletSpeed();

                base.bullets[i].transform.position = Vector3.MoveTowards(
                    base.bullets[i].transform.position,
                    targetList[i].transform.position,
                    Time.deltaTime * bulletSpeed);

                if (Vector3.Distance(base.bullets[i].transform.position, targetList[i].transform.position) < 0.3f)
                {
                    BulletDestroyWithExplosion(base.bullets[i], null);
                }
            }
        }
    }

    public override void ShotBullets(Vector3 _startPos, BUAttackable _origin, OffenceUnit[] _targetUnit, bool isOneTarget)
    {
       base.origin    = _origin;

       maxBulletCount = _targetUnit.Length;

       foreach (var bullet in base.bullets)
       {
           bullet.AwakeBullet(_startPos);
       }

       targetList = new List<OffenceUnit>();
       foreach(var target in _targetUnit)
       {
          targetList.Add(target);
       }

       Vector3 localToWorldDir_left    = _origin.transform.TransformDirection(Vector3.left);
       Vector3 localToWorldDir_right   = _origin.transform.TransformDirection(Vector3.right);
       
       //for (int i = 0; i < bullets.Length; i++)
       //{
       //    Vector3 dirVec = Vector3.zero;
       //    if (i == 1) dirVec = (localToWorldDir_right * Random.Range(1f, 4f));
       //    if (i == 2) dirVec = (localToWorldDir_left  * Random.Range(1f, 4f));
       //    targetList[i] = _targetUnit[i].transform.position + dirVec;
       //}
    }
}
