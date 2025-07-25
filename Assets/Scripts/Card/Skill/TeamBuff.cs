using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBuff : MonoBehaviour
{
    private CardStatus _cardStatus;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyBuffToTeam(CardOwner owner, int buffAmount)
    {
        //Debug.Log($"Applying buff of {buffAmount} to team of {owner}");
        if (owner == CardOwner.Player)
        {
            BattleManager.Instance.PlayerTeamBuff.Top += buffAmount;
            BattleManager.Instance.PlayerTeamBuff.Bottom += buffAmount;
            BattleManager.Instance.PlayerTeamBuff.Left += buffAmount;
            BattleManager.Instance.PlayerTeamBuff.Right += buffAmount;
        }
        else if (owner == CardOwner.Enemy)
        {
            BattleManager.Instance.EnemyTeamBuff.Top += buffAmount;
            BattleManager.Instance.EnemyTeamBuff.Bottom += buffAmount;
            BattleManager.Instance.EnemyTeamBuff.Left += buffAmount;
            BattleManager.Instance.EnemyTeamBuff.Right += buffAmount;
        }
    }
}
