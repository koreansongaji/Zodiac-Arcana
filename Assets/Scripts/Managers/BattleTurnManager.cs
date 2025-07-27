using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnManager : Singleton<BattleTurnManager>
{
    public enum TurnState { PlayerTurn, EnemyTurn }
    public TurnState currentTurn { get; private set; } //�б⸸ ����

    void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        currentTurn = TurnState.PlayerTurn;
        Debug.Log("�÷��̾� �� ����");
        // ī�� ����, �Է� Ȱ��ȭ ��
    }

    public void EndPlayerTurn()
    {
        Debug.Log("�� �� ����");
        StartEnemyTurn();
    }

    public void StartEnemyTurn()
    {
        currentTurn = TurnState.EnemyTurn;
        // AI �ൿ ��
        Invoke(nameof(EndEnemyTurn), 2f); // �� ���� ������ �ڵ����� ���� ��
    }

    public void EndEnemyTurn()
    {
        StartPlayerTurn();
    }
}
