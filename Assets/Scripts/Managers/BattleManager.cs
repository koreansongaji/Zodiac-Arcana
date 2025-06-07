using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { PlayerTurn, EnemyTurn }

public class BattleManager : Singleton<BattleManager>
{
    public TurnState currentTurn { get; private set; } //읽기만 가능

    public List<GameObject> playerCards; // 플레이어 카드 목록
    public List<GameObject> enemyCards; // 적 카드 목록
    public List<GameObject> playerSlots; // 플레이어 슬롯 목록
    public List<GameObject> enemySlots; // 적 슬롯 목록
    public List<GameObject> FieldSlots; // 필드 슬롯 목록
    protected override void Awake()
    {
        base.Awake();
        playerCards = new List<GameObject>();
        enemyCards = new List<GameObject>();
        playerSlots = new List<GameObject>();
        enemySlots = new List<GameObject>();
        FieldSlots = new List<GameObject>();
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
        currentTurn = TurnState.PlayerTurn;
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
        currentTurn = TurnState.EnemyTurn;
        // AI 행동 등
        //Invoke(nameof(EndEnemyTurn), 2f); // 적 턴이 끝나고 자동으로 다음 턴
    }

    public void EndEnemyTurn()
    {
        StartPlayerTurn();
    }
}
