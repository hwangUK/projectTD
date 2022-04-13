using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUIGame : MonoBehaviour
{
    [SerializeField] private GameObject pnlGameOver;
    [SerializeField] private GameObject pnlGameClear;
    [SerializeField] private PnlUnitInfo pnlUnitInfo;

    [SerializeField] private TextMeshProUGUI tmpCountdownTimer;
    [SerializeField] private TextMeshProUGUI tmpGameTimer;
    [SerializeField] private TextMeshProUGUI tmpLife;
    [SerializeField] private TextMeshProUGUI tmpWave;

    [SerializeField] private TextMeshProUGUI tmpSeedCount;
    [SerializeField] private GameObject[] buildBtns;

    public void InitInstance()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSeedCount();
        InitBtnInfo();
        ClosePnlGameOver();
        ClosePnlGameClear();
        ClosePannelUnitInfo();
    }

    public void UpdateSeedCount()
    {
        int count = GameCoreUserData.Instance.CurrentSeedCount;
        tmpSeedCount.text = $"x{count}";
    }

    public void SetTextCountdown(float input = -1f)
    {
        if (input == -1f) 
            tmpCountdownTimer.text = "";
        else
            tmpCountdownTimer.text = input.ToString();
    }
    public void SetTextGameTime(float input = -1f)
    {
        if (input == -1f)
            tmpGameTimer.text = "";
        else
            tmpGameTimer.text = $"Time : {(int)input}"; ((int)input).ToString();
    }
    public void SetTextLife()
    {
        int input = GameCoreUserData.Instance.CurrentLife;
        if (input >= 0)
            tmpLife.text = $"{input}";
    }
    public void UpdateHUDWave()
    {
        int wave = GameCoreUserData.Instance.CurrentWaveIdx;
        tmpWave.text = $"Wave {wave + 1}";
    }

    bool isDubleSpeedMode = false;
    public void DubleSpeedModeSwitch()
    {
        isDubleSpeedMode  = isDubleSpeedMode ? false : true;
        Time.timeScale = isDubleSpeedMode ? 2f : 1f;
    }
    // ======================= PNL
    public void OpenPnlGameOver()
    {
        pnlGameOver.gameObject.SetActive(true);
    }
    public void ClosePnlGameOver()
    {
        pnlGameOver.gameObject.SetActive(false);
    }
    // ------------
    public void OpenPnlGameClear()
    {
        pnlGameClear.gameObject.SetActive(true);
    }
    public void ClosePnlGameClear()
    {
        pnlGameClear.gameObject.SetActive(false);
    }

    //이건 버튼이 아니라 클릭프로시저에서 받아옴
    public void OpenPannelUnitInfo(BuildableUnit src)
    {
        pnlUnitInfo.SetInfo(src);
        UKH.UUtilUI.UIMoveAnimation(pnlUnitInfo.gameObject, true, UKH.UUtilUI.UIMoveDirectionArrow.LeftToMiddle, null);
    }
    //버튼에서 받아오는
    public void ClosePannelUnitInfo() 
    {
        CommonGameScene.Instance.ClickLeaveUnit();
        UKH.UUtilUI.UIMoveAnimation(pnlUnitInfo.gameObject, false, UKH.UUtilUI.UIMoveDirectionArrow.LeftToMiddle, null);
    }

    // -------------------- //
    // ---- 인게임 버튼 ---- //
    // -------------------- //

    //BUILD
    public void InitBtnInfo()
    {
        for (int i = 0; i < buildBtns.Length; i++)
        {
            buildBtns[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                SODataPreset.Instance.CurrentUnitDefenceData((EUnitPresetType)i).iconImg;

            buildBtns[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                SODataPreset.Instance.CurrentUnitDefenceData((EUnitPresetType)i).needSeedCount.ToString();
        }
        UpdateBuildableButtonState();
    }
    public void Build(int idx)
    {
        if (GameCoreUserData.Instance.CurrentSeedCount >=
           SODataPreset.Instance.CurrentUnitDefenceData((EUnitPresetType)idx).needSeedCount)
        {
            CommonGameScene.Instance.SpawnManagerDefence.StartSimulate((EUnitPresetType)idx);
        }
    }
    public void UpdateBuildableButtonState()
    {
        for(int i = 0; i < buildBtns.Length; i++)
        {
            if (GameCoreUserData.Instance.CurrentSeedCount >=
            SODataPreset.Instance.CurrentUnitDefenceData((EUnitPresetType)i).needSeedCount)
            {
                buildBtns[i].GetComponent<Button>().interactable = true;
                buildBtns[i].transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                buildBtns[i].GetComponent<Button>().interactable = false;
                buildBtns[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
    public void CancleCurrentBuildSelected()
    {
        CommonGameScene.Instance.SpawnManagerDefence.CancleSimulate();
    }
}
