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
    public void ApplyBuffToTeam(List<GameObject> teamCards, int buffAmount)
    {
        foreach (GameObject card in teamCards)
        {
            CardStatus cardStatus = card.GetComponent<CardStatus>();
            if (cardStatus != null)
            {
                cardStatus.ChangeStatus((int)buffAmount);
                Debug.Log($"Buff applied to {card.name}: Up={cardStatus.Stats.Up}, Down={cardStatus.Stats.Down}, Left={cardStatus.Stats.Left}, Right={cardStatus.Stats.Right}");
            }
        }
    }
}
