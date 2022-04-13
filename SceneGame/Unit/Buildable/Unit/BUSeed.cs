using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUSeed : BuildableUnit
{
    [SerializeField] GameObject uiEffects;

    // Start is called before the first frame update
    void Start()
    {
        uiEffects.SetActive(false);
    }

    #region Override

    public override void BuildOn()
    {
        base.BuildOn();
        StartCoroutine(CoHarvest());
    }

    public override void UpgradeLevel()
    {
        base.UpgradeLevel();
    }
    public override void SimulateOn()
    {
        base.SimulateOn();
    }
    #endregion

    IEnumerator CoHarvest()
    {
        while(true)
        {
            if(CommonGameScene.Instance.IsGamePlaying)
            {
                yield return GetSeed();
                yield return new WaitForSeconds(base.GetDoingInterval());
            }
            yield return null;
        }
    }
    IEnumerator GetSeed()
    {
        GameCoreUserData.Instance.IncreaseSeed();
        uiEffects.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CommonGameScene.MainUI.UpdateSeedCount();
        CommonGameScene.MainUI.UpdateBuildableButtonState();
        yield return new WaitForSeconds(1f);
        uiEffects.SetActive(false);
    }

}
