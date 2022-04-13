using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneType
{
    Intro,
    Title, 
    Lobby,
    Game_Common,
    Game_Stage_1, 
    Game_Stage_2 , 
    Game_Stage_3 , 
    Game_Stage_4 ,
    Game_Stage_5,
    Game_Stage_6,
    Game_Stage_7,
    Game_Stage_8,
    Game_Stage_9,
    Game_Stage_10
}

public class USceneManager : UKH.USingletoneMonoDontDestroy<USceneManager>
{
    [SerializeField] private List<USceneContainer> sceneList;
    private Dictionary<ESceneType, string> sceneNameTable;
    private ESceneType loadGameSceneIdx;
    private Scene cashAdditiveScene;

    protected override void Awake()
    {
        base.Awake();

        sceneNameTable = new Dictionary<ESceneType, string>();

        foreach(var scene in sceneList)
        {
            sceneNameTable.Add(scene.sceneType, scene.sceneName);
        }
    }

    public void LoadNormalScene(ESceneType sceneType)
    {
        UKH.USceneAsyncLoading.LoadSceneOnLoading(sceneNameTable[sceneType], LoadSceneMode.Single);
    }

    public void LoadGameSceneAdditive(int stageIdx)
    {
        loadGameSceneIdx = GetSceneTypeFromIdx(stageIdx);
        UKH.USceneAsyncLoading.LoadSceneOnLoading(sceneNameTable[ESceneType.Game_Common], LoadSceneMode.Single);
    }

    public void AdditiveLoadGameScene()
    {
        StartCoroutine(CoSceneLoad());
    }

    IEnumerator CoSceneLoad()
    {
        yield return null;
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameTable[loadGameSceneIdx], LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        while (operation.isDone == false)
        {
            yield return null;
            if (operation.progress < 0.9f)
            {
                //TODO  : 프로그레스
            }
            else
            {
                operation.allowSceneActivation = true;
            }
        }
    }

    public void RegisterAdditiveScene(Scene scene)
    {
        cashAdditiveScene = scene;
    }

    public ESceneType GetSceneTypeFromIdx(int stageIdx)
    {
        switch (stageIdx)
        {
            case 1: { return ESceneType.Game_Stage_1; }
            case 2: { return ESceneType.Game_Stage_2; }
            case 3: { return ESceneType.Game_Stage_3; }
            case 4: { return ESceneType.Game_Stage_4; }
            case 5: { return ESceneType.Game_Stage_5; }
            case 6: { return ESceneType.Game_Stage_6; }
            case 7: { return ESceneType.Game_Stage_7; }
            case 8: { return ESceneType.Game_Stage_8; }
            case 9: { return ESceneType.Game_Stage_9; }
            case 10: { return ESceneType.Game_Stage_10;} 
            default: { return ESceneType.Game_Stage_1; } 
        }
    }
}

[System.Serializable]
class USceneContainer
{
    public ESceneType   sceneType;
    public string       sceneName;
}
