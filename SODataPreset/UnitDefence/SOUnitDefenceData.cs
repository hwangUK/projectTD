using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/UnitDefenceData")]
public class SOUnitDefenceData : ScriptableObject
{
    public EUnitPresetType type;
    public Sprite iconImg;

    public float needBuildTime;
    public int needSeedCount;

    public int hp;
    public float doingIntervalTime;
    public float attackDamage;
    public float bulletSpeed;
    public float attackRadius;

    public TextDiscription[] discriptionList;

    [Space(10)]
    [Header("스탯 업그레드 계수")]
    public float statusAmt_hp;
    public int statusAmt_needSeed;
    public float statusAmt_damage;
    public float statusAmt_bulletSpeed;
    public float statusAmt_doingInterval;
    public float statusAmt_attackRadius;
}

[System.Serializable]
public class TextDiscription
{
    public string languageType;

    public string name;
    [TextArea]
    public string discription;
}
