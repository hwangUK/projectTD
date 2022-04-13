using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//public class SceneGame : UKH.USingletoneMono<SceneGame>
//{
//    [SerializeField] private SceneStage             currentStage;
//    [SerializeField] private SpawnManagerOffence    spawnManagerOffence;
//    [SerializeField] private SpawnManagerDefence    spawnManagerDefence;
//    [SerializeField] private MainUIGame mainUI;
//    [SerializeField] private Camera mainCam;

//    public  ParticleSystem[] FXExplotion;

//    private BuildableUnit cashSelectedUnit;

//    private bool isGamePlaying;
//    public SpawnManagerOffence  SpawnManagerOffence     { get => spawnManagerOffence; set => spawnManagerOffence = value; }
//    public SpawnManagerDefence  SpawnManagerDefence     { get => spawnManagerDefence; set => spawnManagerDefence = value; }
//    public static MainUIGame    MainUI                  { get => Instance.mainUI; set => Instance.mainUI = value; }
   
//    public Camera       MainCam         { get => mainCam; set => mainCam = value; }
//    public bool         IsGamePlaying   { get => isGamePlaying; }
//    public SceneStage   CurrentStage    { get => currentStage; set => currentStage = value; }

//    public void GamePlayingOn()         { isGamePlaying = true; }
//    public void GamePlayingOff()        { isGamePlaying = false; }

//    protected override void Awake()
//    {
//        base.Awake();
//        //for(int i = 0; i < stages.Length; i++)
//        //    Stages[i].gameObject.SetActive(false);

//        mainUI.InitInstance();
//    }

//    public void LinkNewStage(SceneStage newStage)
//    {
//        currentStage = newStage;
//    }

//    Ray ray;
//    RaycastHit hit;

//    // Update is called once per frame
//    void Update()
//    {
//        // 오브젝트 선택하기
//        spawnManagerDefence.UUpdate();
//        ProcSelectUnit();
//    }

//    public void ClickOnUnit()
//    {
//        cashSelectedUnit.OnClickObjectProc();
//    }

//    public void ClickLeaveUnit()
//    {
//        if (cashSelectedUnit != null)
//            cashSelectedUnit.OnClickLeave();
//    }

//    public void ProcSelectUnit()
//    {
//        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        if (Input.GetMouseButtonDown(0))// && !IsPointerOverUIObject())
//        {
//            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("unitSelectMeshDefence")))
//            {
//                //방어유닛)
//                cashSelectedUnit = hit.collider.transform.GetComponent<BuildableUnit>();
//                if (cashSelectedUnit != null)
//                    this.ClickOnUnit();
//            }
//            else if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("unitSelectMeshOffence")))
//            {
//                //공격유닛
//                OffenceUnit cashOffenceU = hit.collider.transform.parent.GetComponent<OffenceUnit>();
//                if(cashOffenceU != null)
//                    cashOffenceU.OnClickLeave();
//            }
//        }
//    }

//    // UI터치 시 GameObject 터치 무시하는 코드
//    private bool IsPointerOverUIObject()
//    {
//        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
//        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
//        List<RaycastResult> results = new List<RaycastResult>();
//        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
//        return results.Count > 0;
//    }
//}

////else
////{
////    if (cashSelectedUnit == null) return;
////
////    cashSelectedUnit.HideInfo();
////    cashSelectedUnit = null;
////}