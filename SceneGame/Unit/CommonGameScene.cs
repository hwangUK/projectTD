using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CommonGameScene : UKH.USingletoneMono<CommonGameScene>
{
    [SerializeField] private Game game;

    [SerializeField] private SpawnManagerOffence spawnManagerOffence;
    [SerializeField] private SpawnManagerDefence spawnManagerDefence;
    [SerializeField] private MainUIGame mainUI;

    private BuildableUnit               cashSelectedUnit;
    private bool                        isGamePlaying;

    public SpawnManagerOffence          SpawnManagerOffence { get => spawnManagerOffence; }
    public SpawnManagerDefence          SpawnManagerDefence { get => spawnManagerDefence; }
    public static MainUIGame            MainUI { get => Instance.mainUI; set => Instance.mainUI = value; }

    public bool IsGamePlaying           { get => isGamePlaying; }
    public bool IsLinkGame              { get => game != null; }
    public static Game Game
    {
        get
        {
            if (Instance.game == null)  { Debug.LogError("�������� ��������");  }

            return Instance.game;
        }
    }

    public void GamePlayingOn()         { isGamePlaying = true; }
    public void GamePlayingOff()        { isGamePlaying = false; }

    protected override void Awake()
    {
        base.Awake();                  
        USceneManager.Instance.AdditiveLoadGameScene(); //���Ӱ�������� �ε巹����
        mainUI.InitInstance();
    }
    //for(int i = 0; i < stages.Length; i++) Stages[i].gameObject.SetActive(false);
    public void LinkCurrentStage(Game newStage)
    {
        game = newStage;
    }

    public void ClearCurrentStage()
    {
        game = null;
    }

    Ray ray;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (IsLinkGame == false) return;

        // ������Ʈ �����ϱ�
        spawnManagerDefence.UUpdate();
        ProcSelectUnit();
    }

    public void ClickOnUnit()
    {
        cashSelectedUnit.OnClickObjectProc();
    }

    public void ClickLeaveUnit()
    {
        if (cashSelectedUnit != null)
            cashSelectedUnit.OnClickLeave();
    }

    public void ProcSelectUnit()
    {
        if (IsLinkGame == false) return; 

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))    // && !IsPointerOverUIObject())
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("unitSelectMeshDefence")))
            {
                //�������
                cashSelectedUnit = hit.collider.transform.GetComponent<BuildableUnit>();
                if (cashSelectedUnit != null)
                    this.ClickOnUnit();
            }
            else if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("unitSelectMeshOffence")))
            {
                //��������
                OffenceUnit cashOffenceU = hit.collider.transform.parent.GetComponent<OffenceUnit>();
                if (cashOffenceU != null)
                    cashOffenceU.OnClickLeave();
            }
        }
    }

    // UI��ġ �� GameObject ��ġ �����ϴ� �ڵ�
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}