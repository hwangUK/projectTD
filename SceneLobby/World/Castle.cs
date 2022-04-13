using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Castle : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] GameObject road;
    [SerializeField] Color deactiveColor;
    [SerializeField] Color activeColor;

    // Start is called before the first frame update
    void Start()
    {
        road.GetComponent<MeshRenderer>().material.color = deactiveColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickCastle()
    {
        SceneLobby.Instance.MainUI.OpenPannelCastleInfo(index);
    }

    public void OpenNextStage()
    {
        road.GetComponent<MeshRenderer>().material.DOColor(activeColor, 1f);
    }
}
