using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLobby : UKH.USingletoneMono<SceneLobby>
{
    [SerializeField] MainUILobby mainUI;

    [SerializeField] GameObject movementDummy;
    [SerializeField] Camera     camera;

    [SerializeField] GameObject[] castles;

    [SerializeField] Vector3 cameraOffset;

    public bool cameraBondToDummy;

    public MainUILobby MainUI { get => mainUI; set => mainUI = value; }

    // Start is called before the first frame update
    void Start()
    {
        LoadLobby();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cameraBondToDummy)
            camera.transform.position = Vector3.Lerp(camera.transform.position, movementDummy.transform.position + (cameraOffset), Time.deltaTime);
    }

    void LoadLobby()
    {
        //현재위치로
        movementDummy.transform.position = castles[GameCoreUserData.Instance.CurrentStageIdx].transform.position;
        cameraBondToDummy = true;
    }
}
