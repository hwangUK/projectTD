using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PnlCastleInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtCastleName;

    int stageIdx;
    public void SetInfo(int _stageIdx)
    {
        switch (_stageIdx)
        {
            case 0:
                {
                    txtCastleName.text = "Little Forest";
                }
                break;
            case 1:
                {
                    txtCastleName.text = "Middle Forest";
                }
                break;
        }
    }
    
    public void StartGame()
    {
        GameCoreUserData.Instance.ResetGameValue(stageIdx);
        USceneManager.Instance.LoadGameSceneAdditive(stageIdx);
    }
}
