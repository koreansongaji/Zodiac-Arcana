using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TurnState { PlayerTurn, EnemyTurn }

public class BattleManager : Singleton<BattleManager>
{
    public TurnState CurrentTurn { get; private set; } //읽기만 가능
    public int stage; // 현재 스테이지 번호

    [Tooltip("플레이어나 적이 한 번 행동하면 라운드가 증가")]
    public int Round; // 플레이어나 적이 한 번 행동하면 라운드가 증가
    [Header("턴 시간")]
    public float PlayerTime = 31.0f;
    public float EnemyTime  = 31.0f;

    [Header("카드 정보")]
    public List<GameObject> PlayerCards; // 플레이어 카드 목록
    public List<GameObject> EnemyCards; // 적 카드 목록
    public List<GameObject> PlayerSlots; // 플레이어 슬롯 목록
    public List<GameObject> EnemySlots; // 적 슬롯 목록
    public List<GameObject> FieldSlots; // 필드 슬롯 목록
    public BattleUI BattleUI; // 배틀 UI 스크립트

    [Header("버프 정보")]
    public CardStats PlayerTeamBuff = new CardStats(); // 플레이어 팀 버프
    public CardStats EnemyTeamBuff = new CardStats(); // 적 팀 버프
    protected override void Awake()
    {
        base.Awake();
        PlayerCards = new List<GameObject>();
        EnemyCards = new List<GameObject>();
        PlayerSlots = new List<GameObject>();
        EnemySlots = new List<GameObject>();
        FieldSlots = new List<GameObject>();
        Round = 0;
        stage = 0;
        GameManager.Instance.SaveDataLoad(); // 게임 매니저에서 저장 데이터 불러오기
        stage = GameManager.Instance.StageData.Stage; // 게임 매니저에서 스테이지 정보 가져오기
        PlayerTeamBuff = new CardStats(0, 0, 0, 0);
        EnemyTeamBuff = new CardStats(0, 0, 0, 0);
        CurrentTurn = TurnState.EnemyTurn; // 초기 턴은 플레이어 턴
        PlayerTime = 31.0f; // 플레이어 턴 시간
        EnemyTime = 31.0f; // 적 턴 시간
    }
    void Start()
    {
        StartEnemyTurn();
    }
    private void OnEnable()
    {
    }
    private void FixedUpdate()
    {
        if(!CheckStage())
        {
            return; // 스테이지가 아닌 경우 업데이트 중지
        }
        if(CurrentTurn == TurnState.PlayerTurn)
        {
            PlayerTime -= Time.fixedDeltaTime;
            if (PlayerTime <= 0)
            {
                PlayerTime = 0;
                EndTime();
            }
        }
        else if(CurrentTurn == TurnState.EnemyTurn)
        {
            EnemyTime -= Time.fixedDeltaTime;
            if (EnemyTime <= 0)
            {
                EnemyTime = 0;

            }
        }
    }
    
    private void GetInfo()
    {

    }
    public void StartPlayerTurn()
    {
        CurrentTurn = TurnState.PlayerTurn;
        StartTurn();
        //Debug.Log("플레이어 턴 시작");
        // 카드 선택, 입력 활성화 등
    }

    public void EndPlayerTurn()
    {
        //Debug.Log("적 턴 시작");
        StartEnemyTurn();
    }

    public void StartEnemyTurn()
    {
        CurrentTurn = TurnState.EnemyTurn;
        StartTurn();

        // AI 행동 등
        //Invoke(nameof(EndEnemyTurn), 2f); // 적 턴이 끝나고 자동으로 다음 턴
    }

    public void EndEnemyTurn()
    {
        StartPlayerTurn();
    }
    public void ResetStage(int stage)
    {
        CurrentTurn = TurnState.EnemyTurn; // 스테이지 리셋 시 적 턴으로 초기화
        this.stage = stage;
        Round = 0;
        PlayerCards.Clear();
        EnemyCards.Clear();
        PlayerSlots.Clear();
        EnemySlots.Clear();
        FieldSlots.Clear();
        
        //Debug.Log($"스테이지 {Stage} 시작");
        StartEnemyTurn();
    }
    public void EndStage()
    {
        if (PlayerCards.Count > EnemyCards.Count)
        {
            Debug.Log($"스테이지 {stage} 클리어! 플레이어 승리!");
            PlayerWin();
        }
        else
        {
            Debug.Log($"스테이지 {stage} 실패! 적 승리!");
            PlayerLose();
        }
    }
    public bool CheckStage()
    {
        return ((SceneManager.GetActiveScene().name == "Stage 1") || (SceneManager.GetActiveScene().name == "Stage 2")
            || (SceneManager.GetActiveScene().name == "Stage 3") || (SceneManager.GetActiveScene().name == "Stage 4")
            || (SceneManager.GetActiveScene().name == "Stage 5"));
    }
    public void StartTurn()
    {
        if(!CheckStage())
        {
            return;
        }
        Round++;
        Debug.Log($"라운드: {Round}");
        //Debug.Log($"스테이지 {Stage} 라운드 {Round} 시작");

        //Debug.Log($"턴 시작: {CurrentTurn}");
        PlayerTeamBuff = new CardStats(0, 0, 0, 0);
        EnemyTeamBuff = new CardStats(0, 0, 0, 0);
        foreach (var card in PlayerCards)
        {
            //if((LayerMask.GetMask("FieldCardSlot") & (1<<card.GetComponent<CardStatus>().CurrentSlot.layer)) != 0)
            //{
                //Debug.Log($"카드 {card.name}의 능력 사용");
                card.GetComponent<Card>().UseSkill();
            //}
        }
        foreach (var card in EnemyCards)
        {
            //if ((LayerMask.GetMask("FieldCardSlot") & (1 << card.GetComponent<CardStatus>().CurrentSlot.layer)) != 0)
            //{
                //Debug.Log($"적 카드 {card.name}의 능력 사용");
                card.GetComponent<Card>().UseSkill();
            //}
        }

        foreach (var card in PlayerCards)
        {
            //if ((LayerMask.GetMask("FieldCardSlot") & (1 << card.GetComponent<CardStatus>().CurrentSlot.layer)) != 0)
            //{
                //Debug.Log($"플레이어 카드 {card.name}의 능력 사용");
                card.GetComponent<CardStatus>().ChangeStatus(PlayerTeamBuff);
            //}
        }
        foreach (var card in EnemyCards)
        {
            //if ((LayerMask.GetMask("FieldCardSlot") & (1 << card.GetComponent<CardStatus>().CurrentSlot.layer)) != 0)
            //{
                //Debug.Log($"적 카드 {card.name}의 능력 사용");
                card.GetComponent<CardStatus>().ChangeStatus(EnemyTeamBuff);
            //}
        }
        if(BattleManager.Instance.BattleUI != null)
        {
            BattleManager.Instance.BattleUI.CardCount();
        }

        int emptyFieldSlots = 0;
        foreach (var slot in FieldSlots)
        {
            if (slot.GetComponent<SlotInfo>().OccupiedCard == null)
            {
                emptyFieldSlots++;
            }
        }

        if ((Round >= 10) || emptyFieldSlots == 0)
        {
            Debug.Log("라운드 10에 도달했습니다. 스테이지를 종료합니다.");
            EndStage(); // 라운드 10에 도달하면 스테이지 종료
            return;
        }
        //--------------- 턴 시간 초기화 -----------------
        if (CurrentTurn == TurnState.PlayerTurn)
        {
            PlayerTime = 31.0f; // 플레이어 턴 시간 초기화
        }
        else if(CurrentTurn == TurnState.EnemyTurn)
        {
            EnemyTime = 31.0f; // 적 턴 시간 초기화
        }
        if(BattleUI != null)
        {
            BattleUI.ChangeTurnIndicator(CurrentTurn);
        }
    }
    public void EndTime()
    {
        if(CurrentTurn == TurnState.PlayerTurn)
        {
            CurrentTurn = TurnState.EnemyTurn; // 플레이어 턴이 끝나면 적 턴으로 전환

            GameObject selectCard = null;
            GameObject emptySlot = null;
            foreach (var slot in PlayerSlots)
            {
                if (slot.GetComponent<SlotInfo>().OccupiedCard != null)
                {
                    selectCard = slot.GetComponent<SlotInfo>().OccupiedCard;
                    slot.GetComponent<SlotInfo>().OccupiedCard = null; // 슬롯에서 카드 제거
                    break; // 선택된 카드가 있으면 반복문 종료
                }
            }
            foreach (var slot in FieldSlots)
            {
                if (slot.GetComponent<SlotInfo>().OccupiedCard == null)
                {
                    emptySlot = slot;
                    slot.GetComponent<SlotInfo>().OccupiedCard = null; // 슬롯에 카드 할당
                    break; // 빈 슬롯이 있으면 반복문 종료
                }
            }
            selectCard.GetComponent<CardStatus>().CurrentSlot.GetComponent<SlotInfo>().OccupiedCard = null; // 카드의 현재 슬롯에서 카드 제거
            selectCard.transform.position = emptySlot.transform.position;
            selectCard.GetComponent<SetPositionCard>().SetDropCard();
            selectCard.GetComponent<Card>().CompareWithAdjacentCards();
            emptySlot.GetComponent<SlotInfo>().OccupiedCard = selectCard; // 슬롯에 카드 할당
            EndPlayerTurn();
        }
    }
    public void PlayerWin()
    {
        SetStageLevel();
        //StartCoroutine(GoToLevelSelevt()); // 2초 후 레벨 선택 화면으로 이동
        if(SceneManager.GetActiveScene().name.Equals("Stage 5"))
        {
            SceneManager.LoadScene("Ending Cutscene");
        }
        else
        {
            SceneManager.LoadScene("Level Select");
        }

    }
    public void PlayerLose()
    {
        //StartCoroutine(GoToLevelSelevt()); // 2초 후 레벨 선택 화면으로 이동
        SceneManager.LoadScene("Level Select");
    }

    private void SetStageLevel()
    {
        string stageLevel = SceneManager.GetActiveScene().name;

        switch (stageLevel)
        {
            case "Stage 1":
                stage = 1;
                break;
            case "Stage 2":
                stage = 2;
                break;
            case "Stage 3":
                stage = 3;
                break;
            case "Stage 4":
                stage = 4;
                break;
            case "Stage 5":
                stage = 5;
                break;
            default:
                stage = 0; // 기본값
                break;
        }

        if(stage > GameManager.Instance.StageData.Stage)
        {
            GameManager.Instance.StageData.Stage = stage; // 현재 스테이지가 저장된 스테이지보다 높으면 업데이트
            GameManager.Instance.SaveDataSave(); // 저장 데이터 저장
        }
    }
    IEnumerator GoToLevelSelevt()
    {
        yield return new WaitForSeconds(2f); // 1초 대기
        ResetStage(GameManager.Instance.StageData.Stage); // 스테이지 초기화
        SceneManager.LoadScene("Level Select"); // 레벨 선택 화면으로 이동
    }
}
