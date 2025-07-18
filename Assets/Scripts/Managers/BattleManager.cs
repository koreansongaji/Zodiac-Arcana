using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { PlayerTurn, EnemyTurn }

public class BattleManager : Singleton<BattleManager>
{
    public TurnState CurrentTurn { get; private set; } //읽기만 가능
    public int Stage; // 현재 스테이지 번호
    public int Round;

    public List<GameObject> PlayerCards; // 플레이어 카드 목록
    public List<GameObject> EnemyCards; // 적 카드 목록
    public List<GameObject> PlayerSlots; // 플레이어 슬롯 목록
    public List<GameObject> EnemySlots; // 적 슬롯 목록
    public List<GameObject> FieldSlots; // 필드 슬롯 목록
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
    }
    void Start()
    {
        StartPlayerTurn();
    }

    private void GetInfo()
    {

    }
    public void StartPlayerTurn()
    {
        CurrentTurn = TurnState.PlayerTurn;
        Debug.Log("플레이어 턴 시작");
        // 카드 선택, 입력 활성화 등
    }

    public void EndPlayerTurn()
    {
        Debug.Log("적 턴 시작");
        StartEnemyTurn();
    }

    public void StartEnemyTurn()
    {
        CurrentTurn = TurnState.EnemyTurn;
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
        Debug.Log($"스테이지 {Stage} 시작");
        StartPlayerTurn();
    }
}
