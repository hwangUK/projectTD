using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletSystemBase : MonoBehaviour
{
    protected BUAttackable  origin;
    protected Bullet[]      bullets;

    public bool isActive;
    public abstract void ProcMove();

    //하나쏘는애도있고 여러명 쏘는애도있어
    public abstract void ShotBullets(Vector3 _startPos, BUAttackable _origin, OffenceUnit[] _targetUnit, bool isOneTarget);

    private void Start()
    {
        isActive = false;
    }
    public void BulletAwake()
    {
        isActive = true;
        bullets = this.GetComponentsInChildren<Bullet>(true);
        foreach (var bullet in bullets)
        {
            bullet.AwakeBullet(this.transform.position);
        }
    }
    public void BulletDestroyWithExplosion(Bullet _bullet, OffenceUnit target)
    {
        foreach (var bullet in bullets)
        {
            if(bullet == _bullet)
            {
                bullet.transform.localPosition = Vector3.zero;
                bullet.BulletDestroyExplosion(target == null ? null : target.transform);
                break;
            }
        }
        this.CheckAllBulletsOff();
    }
    private void CheckAllBulletsOff()
    {
        int cnt = bullets.Length;
        foreach (var bullet in bullets)
        {
            if (bullet.gameObject.activeSelf == false) cnt--; 
        }
        isActive = cnt == 0 ? false : true;
    }

    private void Update()
    {
        ProcMove();
    }

    public void Explosion(OffenceUnit target, Bullet bullet)
    {
        target.ChangeMaterial(OBJDataPreset.Instance.fXMaterials.white, 0.5f, origin,
            (origin) => { target.Damaged(origin); });

        BulletDestroyWithExplosion(bullet, target);
    }
}
