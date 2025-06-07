using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { PlayerTurn, EnemyTurn }

public class BattleManager : Singleton<BattleManager>
{
    public TurnState currentTurn { get; private set; } //�б⸸ ����

    public List<GameObject> playerCards; // �÷��̾� ī�� ���
    public List<GameObject> enemyCards; // �� ī�� ���
    public List<GameObject> playerSlots; // �÷��̾� ���� ���
    public List<GameObject> enemySlots; // �� ���� ���
    public List<GameObject> FieldSlots; // �ʵ� ���� ���
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
        //Invoke(nameof(EndEnemyTurn), 2f); // �� ���� ������ �ڵ����� ���� ��
    }

    public void EndEnemyTurn()
    {
        StartPlayerTurn();
    }
}
