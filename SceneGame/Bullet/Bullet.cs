using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    BulletSystemBase root;
    [SerializeField] ParticleSystem particleExplosion;
    [SerializeField] ParticleSystem particleTrail;

    private bool isMove = false;
    public BulletSystemBase Root { get => this.transform.parent.GetComponent<BulletSystemBase>(); }
    public bool IsMove { get => isMove; }

    public void BulletDamageToOffenceUnit(OffenceUnit targetUnit)
    {
        Root.Explosion(targetUnit, this);
        this.BulletDestroyExplosion(targetUnit.transform);
    }
    public void AwakeBullet(Vector3 _startPos)
    {
        isMove = true;
        this.OnFXTrail();
        this.OffFXExplosion();

        transform.position = _startPos;
        isMove = true;
        this.gameObject.SetActive(true);

    }
    public void BulletDestroyExplosion(Transform targetPos)
    {
        isMove = false;

        this.OffFXTrail();

        if(targetPos != null)
        {
            this.OnFXExplosion();
            particleExplosion.transform.position = targetPos.position + Vector3.up * 2f;
        }

        this.gameObject.SetActive(false);
    }
    public void OnFXExplosion() { particleExplosion.Play(); }
    public void OffFXExplosion() { particleExplosion.Stop(); }
    public void OnFXTrail() { particleTrail.Play(); }
    public void OffFXTrail() { particleTrail.Stop(); }

    private void OnDisable()
    {
        BulletDestroyExplosion(null);
    }
}
