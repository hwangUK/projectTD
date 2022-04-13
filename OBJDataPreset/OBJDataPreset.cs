using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJDataPreset : UKH.USingletoneMono<OBJDataPreset>
{
    public FXMaterials fXMaterials;
    protected override void Awake()
    {
        base.Awake();
    }
}

[System.Serializable]
public class FXMaterials
{
    public Material defLit;
    public Material defUnLit;
    public Material attackBoundery;
    public Material black;
    public Material peach;
    public Material red;
    public Material solid;
    public Material white;

    public Material alpha;
    public Material fx_rim;
    public Material fx_glow;
    //[SerializeField] Material[] materials;
    //public Dictionary<string,Material> materialsTable;
    //FXMaterials()
    //{
    //    materialsTable = new Dictionary<string, Material>();
    //}
}

