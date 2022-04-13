using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlUnitInfo : MonoBehaviour
{
    [SerializeField] Text txt_needSeedCount;
    [SerializeField] Text txt_nameTitle;
    [SerializeField] Text txt_level;
    [SerializeField] Text txt_damage;
    [SerializeField] Text txt_attackSpeed;
    [SerializeField] Text txt_attackRange;
    [SerializeField] Text txt_discription;

    BuildableUnit cashUnit;

    public void SetInfo(BuildableUnit src)
    {
        txt_needSeedCount.text  = $"x{src.GetUpgradeNeedSeedCount}";
        txt_nameTitle.text      = $"{src.OriginData.discriptionList[0].name}";
        txt_level.text          = $"���� : {src.Level+1}";
        txt_discription.text    = $"{src.OriginData.discriptionList[0].discription}";

        if (src is BUAttackable)
        {
            BUAttackable cast = src as BUAttackable;
            txt_damage.text      = $"������ : {cast.GetAttackDamage()}";
            txt_attackRange.text = $"�����Ÿ� : {cast.GetAttackRadius()}";
            txt_attackSpeed.text = $"���ݼӵ� : {src.GetDoingInterval()}";
        }
        else
        {
            txt_damage.text      = "";
            txt_attackRange.text = "";
            txt_attackSpeed.text = $"���� ��Ȯ�ӵ� : {src.GetDoingInterval()}";
        }

        cashUnit = src;
    }

    public void UpgradeLevel()
    {
        GameCoreUserData.Instance.ReduceSeedCount(cashUnit.GetUpgradeNeedSeedCount);
        cashUnit.UpgradeLevel();
        SetInfo(cashUnit);

        CommonGameScene.MainUI.UpdateBuildableButtonState();
        CommonGameScene.MainUI.UpdateSeedCount();
    }

    private void OnDisable()
    {
        cashUnit = null;
    }
}
