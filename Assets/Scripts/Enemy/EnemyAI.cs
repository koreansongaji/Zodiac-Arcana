using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyCards;
    [SerializeField] private List<GameObject> _enemyHandSlots;
    public List<GameObject> EnemyHandCards; // List of enemy cards
    public List<GameObject> EmptySlots;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
        //ResetEnemyAI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (BattleManager.Instance.CurrentTurn == TurnState.EnemyTurn)
        {
            StartAITurn();
        }
    }
    //private void ResetEnemyAI()
    //{
    //    EnemyHandCards.Clear();
    //    EnemyHandCards.AddRange(_enemyCards);
    //}
    public void StartAITurn()
    {
        EnemyHandCards.Clear();
        EmptySlots.Clear();

        foreach (var slot in _enemyHandSlots)
        {
            if (slot.GetComponent<SlotInfo>().OccupiedCard != null)
            {
                EnemyHandCards.Add(slot.GetComponent<SlotInfo>().OccupiedCard);
            }
        }

        foreach (var slot in BattleManager.Instance.FieldSlots)
        {
            if (slot.GetComponent<SlotInfo>().OccupiedCard == null)
            {
                EmptySlots.Add(slot);
            }
        }
        if(EnemyHandCards.Count == 0 || EmptySlots.Count == 0)
        {
            BattleManager.Instance.EndEnemyTurn(); // End turn if no cards or slots available
            return;
        }
        GameObject selectedCard = EnemyHandCards[Random.Range(0, EnemyHandCards.Count)];
        GameObject selectedSlot = EmptySlots[Random.Range(0, EmptySlots.Count)];

        if(selectedCard.GetComponent<CardStatus>().CurrentSlot.GetComponent<SlotInfo>().OccupiedCard != null)
        {
            selectedCard.GetComponent<CardStatus>().CurrentSlot.GetComponent<SlotInfo>().OccupiedCard = null; // Clear the current slot of the card
        }

        selectedCard.transform.position = selectedSlot.transform.position;
        selectedCard.GetComponent<SetPositionCard>().SetDropCard();
        EnemyHandCards.Remove(selectedCard); // Remove the card from hand after placing it
        BattleManager.Instance.EndEnemyTurn(); // End enemy turn after placing the card
    }
}
