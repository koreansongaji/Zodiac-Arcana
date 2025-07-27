using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TurnState { PlayerTurn, EnemyTurn }

public class BattleManager : Singleton<BattleManager>
{
    public TurnState CurrentTurn { get; private set; } //읽기만 가능
    public int Stage; // 현재 스테이지 번호
    public int Round;
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
        Stage = 0;
        PlayerTeamBuff = new CardStats(0, 0, 0, 0);
        EnemyTeamBuff = new CardStats(0, 0, 0, 0);
        CurrentTurn = TurnState.EnemyTurn; // 초기 턴은 플레이어 턴
        PlayerTime = 31.0f; // 플레이어 턴 시간
        EnemyTime = 31.0f; // 적 턴 시간
    }
    void Start()
    {
        StartPlayerTurn();
    }
    private void FixedUpdate()
    {
        if(CurrentTurn == TurnState.PlayerTurn)
        {
            PlayerTime -= Time.fixedDeltaTime;
            if (PlayerTime <= 0)
            {
                PlayerTime = 0;
                PlayerLose(); // 플레이어 턴 시간 초과 시
            }
        }
        else if(CurrentTurn == TurnState.EnemyTurn)
        {
            EnemyTime -= Time.fixedDeltaTime;
            if (EnemyTime <= 0)
            {
                EnemyTime = 0;
                PlayerWin(); // 적 턴 시간 초과 시
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
    public void StartStage(int stage)
    {
        Stage = stage;
        Round = 0;
        PlayerCards.Clear();
        EnemyCards.Clear();
        PlayerSlots.Clear();
        EnemySlots.Clear();
        FieldSlots.Clear();
        //Debug.Log($"스테이지 {Stage} 시작");
        StartPlayerTurn();
    }
    public void StartTurn()
    {
        //Debug.Log($"턴 시작: {CurrentTurn}");
        PlayerTeamBuff = new CardStats(0, 0, 0, 0);
        EnemyTeamBuff = new CardStats(0, 0, 0, 0);
        foreach (var card in PlayerCards)
        {
            if((LayerMask.GetMask("FieldCardSlot") & (1<<card.GetComponent<CardStatus>().CurrentSlot.layer)) != 0)
            {
                //Debug.Log($"카드 {card.name}의 능력 사용");
                card.GetComponent<Card>().UseSkill();
            }
        }
        foreach (var card in EnemyCards)
        {
            if ((LayerMask.GetMask("FieldCardSlot") & (1 << card.GetComponent<CardStatus>().CurrentSlot.layer)) != 0)
            {
                //Debug.Log($"적 카드 {card.name}의 능력 사용");
                card.GetComponent<Card>().UseSkill();
            }
        }

        foreach (var card in PlayerCards)
        {
            if ((LayerMask.GetMask("FieldCardSlot") & (1 << card.GetComponent<CardStatus>().CurrentSlot.layer)) != 0)
            {
                //Debug.Log($"플레이어 카드 {card.name}의 능력 사용");
                card.GetComponent<CardStatus>().ChangeStatus(PlayerTeamBuff);
            }
        }
        foreach (var card in EnemyCards)
        {
            if ((LayerMask.GetMask("FieldCardSlot") & (1 << card.GetComponent<CardStatus>().CurrentSlot.layer)) != 0)
            {
                //Debug.Log($"적 카드 {card.name}의 능력 사용");
                card.GetComponent<CardStatus>().ChangeStatus(EnemyTeamBuff);
            }
        }

        //--------------- 턴 시간 초기화 -----------------
        if(CurrentTurn == TurnState.PlayerTurn)
        {
            PlayerTime = 31.0f; // 플레이어 턴 시간 초기화
        }
        else if(CurrentTurn == TurnState.EnemyTurn)
        {
            EnemyTime = 31.0f; // 적 턴 시간 초기화
        }

        BattleUI.ChangeTurnIndicator(CurrentTurn);
    }
    public void PlayerWin()
    {

    }
    public void PlayerLose()
    {

    }
}
