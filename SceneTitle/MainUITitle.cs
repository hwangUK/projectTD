using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUITitle : MonoBehaviour
{
    public void GoToLobbyScene()
    {
        USceneManager.Instance.LoadNormalScene(ESceneType.Lobby);
    }
}
