using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystemWide : BulletSystemBase
{
    protected Vector3[] targetpos;
    public float separation = 3f;

    public override void ShotBullets(Vector3 _startPos, BUAttackable _origin, OffenceUnit[] _targetPos, bool isOneTarget)
    {
        base.origin = _origin;

        foreach (var bullet in base.bullets)
        {
            bullet.AwakeBullet(_startPos);
        }
        targetpos   = new Vector3[bullets.Length];

        Vector3 localToWorldDir_left = _origin.transform.TransformDirection(Vector3.left);
        Vector3 localToWorldDir_right = _origin.transform.TransformDirection(Vector3.right);
        for (int i = 0; i < bullets.Length; i++)
        {
            Vector3 dirVec = Vector3.zero;
            if (i == 1) dirVec = (localToWorldDir_right * separation);
            if (i == 2) dirVec = (localToWorldDir_left * separation);
            if (i == 3) dirVec = (localToWorldDir_right * (separation * 2));
            if (i == 4) dirVec = (localToWorldDir_left * (separation * 2));
            if (i == 5) dirVec = (localToWorldDir_right * (separation * 3));
            if (i == 6) dirVec = (localToWorldDir_left * (separation * 3));

            targetpos[i] = _targetPos[0].transform.position + dirVec;
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
                base.bullets[i].transform.position = Vector3.MoveTowards(
                base.bullets[i].transform.position,
                targetpos[i],
                Time.deltaTime * bulletSpeed);
                if (Vector3.Distance(base.bullets[i].transform.position, targetpos[i]) < 0.3f)
                {
                    BulletDestroyWithExplosion(base.bullets[i], null);
                }
            }
        }
    }
}
