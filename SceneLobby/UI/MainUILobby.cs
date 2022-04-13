using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUILobby : MonoBehaviour
{
    [SerializeField] PnlCastleInfo pnlCastleInfo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPannelCastleInfo(int stageIdx)
    {
        UKH.UUtilUI.UIMoveAnimation(pnlCastleInfo.gameObject, true, UKH.UUtilUI.UIMoveDirectionArrow.RightToMiddle, null);
        pnlCastleInfo.SetInfo(stageIdx);
    }
    public void ClosePannelCastleInfo()
    {
        UKH.UUtilUI.UIMoveAnimation(pnlCastleInfo.gameObject, false, UKH.UUtilUI.UIMoveDirectionArrow.RightToMiddle, null);
    }
}
