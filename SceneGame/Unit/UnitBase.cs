using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void CallbackChangeMat<T>(T buildableUnit);

public abstract class UnitBase : UKH.UPool
{
    [SerializeField] private GameObject     outline;
    [SerializeField] private MeshRenderer   renderer;

    [SerializeField] private float speed;

    private IEnumerator _coChangeMat;
    private Material cashOriginMat;
    protected float freezeSpeed;

    public void SetSpeed(float newSpeed)
    {
        freezeSpeed = newSpeed;
    }
    public void SetSpeedUp(float newSpeed)
    {
        freezeSpeed += newSpeed;
    }
    public void SetSpeedFreeze()
    {
        freezeSpeed = 0f;
    }
    public void SetSpeedOrigin()
    {
        freezeSpeed = speed;
    }
    protected virtual void OnClickObjectProc()
    {
        this.OutlineOn();
    }
    protected virtual void OnClickLeave()
    {
        this.OutlineOff();
    }

    protected void InitInstance()
    {
        speed = speed == 0f ? 5f : speed;
        cashOriginMat = renderer.material;
        this.SetSpeedOrigin();
        this.OutlineOff();
    }

    public void ChangeMaterial(Material src)
    {
        renderer.material = src;
    }
    public void ChangeMaterialOrigin()
    {
        renderer.material = cashOriginMat;
    }

    public void ChangeMaterial(Material src, float duration, BuildableUnit param = null, CallbackChangeMat<BuildableUnit> callback = null)
    {
        if (_coChangeMat != null) StopCoroutine(_coChangeMat);
        _coChangeMat = CoChangeMaterial(src, duration, param, callback);
        StartCoroutine(_coChangeMat);
    }
    IEnumerator CoChangeMaterial(Material src, float duration, BuildableUnit param, CallbackChangeMat<BuildableUnit> callback)
    {
        renderer.material = src;
        yield return new WaitForSeconds(duration);
        renderer.material = cashOriginMat;

        callback(param);
    }
    public void OutlineOn()
    {
        outline.gameObject.SetActive(true);
    }

    public void OutlineOff()
    {
        outline.gameObject.SetActive(false);
    }
}
