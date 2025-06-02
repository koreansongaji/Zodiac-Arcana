using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnManager : Singleton<BattleTurnManager>
{
    public enum TurnState { PlayerTurn, EnemyTurn }
    public TurnState currentTurn { get; private set; } //읽기만 가능

    void Start()
    {
        StartPlayerTurn();
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
        Invoke(nameof(EndEnemyTurn), 2f); // 적 턴이 끝나고 자동으로 다음 턴
    }

    public void EndEnemyTurn()
    {
        StartPlayerTurn();
    }
}
