using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystemNomal : BulletSystemBase
{
    Vector3 targetpos;

    public override void ShotBullets(Vector3 _startPos, BUAttackable _origin, OffenceUnit[] _targetUnit, bool isOneTarget)
    {
        base.origin = _origin;
        targetpos   = _targetUnit[0].transform.position;
        foreach (var bullet in base.bullets)
        {
            bullet.AwakeBullet(_startPos);
        }
    }

    public override void ProcMove()
    {
        if (base.bullets == null) return;

        for (int i = 0; i < base.bullets.Length; i++)
        {
            Bullet cash = base.bullets[i];

            if (cash.IsMove)
            {
                float bulletSpeed = origin.GetBulletSpeed();
                cash.transform.position = Vector3.MoveTowards(
                    cash.transform.position,
                    targetpos,
                    Time.deltaTime * bulletSpeed);
                if (Vector3.Distance(cash.transform.position, targetpos) < 0.1f)
                {
                    BulletDestroyWithExplosion(cash, null);
                }
            }
        }
    }
}
