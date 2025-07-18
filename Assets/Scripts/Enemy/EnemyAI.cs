using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public struct CardWeight
    {
        public GameObject CurrentCard;
        public GameObject MaxWeightSlot;
        public int Weight;
    }
    [SerializeField] int Stage;

    [SerializeField] private List<GameObject> _enemyCards = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemyHandSlots = new List<GameObject>();
    public List<CardWeight> EnemyHandCards = new List<CardWeight>();
    public List<GameObject> EmptySlots = new List<GameObject>();
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
            CardWeight cardWeight = new CardWeight
            {
                CurrentCard = null,
                MaxWeightSlot = null,
                Weight = 0
            };
            if (slot.GetComponent<SlotInfo>().OccupiedCard != null)
            {
                cardWeight.CurrentCard = slot.GetComponent<SlotInfo>().OccupiedCard; // Get the card in the slot
            }
            EnemyHandCards.Add(cardWeight); // Initialize the list with empty card weights
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

        GameObject selectedCard = EnemyHandCards[Random.Range(0, EnemyHandCards.Count)].CurrentCard;
        GameObject selectedSlot = EmptySlots[Random.Range(0, EmptySlots.Count)];

        if (BattleManager.Instance.Stage == 1)
        {
            if (BattleManager.Instance.Round <= 2)
            {
                selectedCard = EnemyHandCards[Random.Range(0, EnemyHandCards.Count)].CurrentCard; // Randomly select a card from hand
                selectedSlot = EmptySlots[Random.Range(0, EmptySlots.Count)]; // Randomly select an empty slot
            }
        }
        else
        {
            for (int index = 0; index < EnemyHandCards.Count; index++)
            {
                if(EnemyHandCards[index].CurrentCard == null)
                {
                    continue; // Skip if the card is null
                }

                int maxWeight = 0;
                GameObject maxWeightSlot = null;
                var card = EnemyHandCards[index].CurrentCard;

                foreach (var slot in EmptySlots)
                {
                    int weight = 0; // Reset weight for each slot
//-------------------------------- 뒤집을 수 있는가 가중치 계산 ----------------------------------
                    if ((slot.GetComponent<SlotInfo>().LinkedSlots.leftSlot != null) && 
                        (slot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>().OccupiedCard != null) &&
                        (card.GetComponent<CardStatus>().Stats.Left > 
                        slot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Stats.Right))
                    {
                        weight += card.GetComponent<CardStatus>().CardData.FlipCountWeight; // Increase weight if the card can beat the left adjacent card
                    }
                    if ((slot.GetComponent<SlotInfo>().LinkedSlots.topSlot != null) &&
                        (slot.GetComponent<SlotInfo>().LinkedSlots.topSlot.GetComponent<SlotInfo>().OccupiedCard != null) &&
                        (card.GetComponent<CardStatus>().Stats.Top >
                        slot.GetComponent<SlotInfo>().LinkedSlots.topSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Stats.Bottom))
                    {
                        weight += card.GetComponent<CardStatus>().CardData.FlipCountWeight;
                    }
                    if ((slot.GetComponent<SlotInfo>().LinkedSlots.rightSlot != null) &&
                        (slot.GetComponent<SlotInfo>().LinkedSlots.rightSlot.GetComponent<SlotInfo>().OccupiedCard != null) &&
                        (card.GetComponent<CardStatus>().Stats.Right >
                        slot.GetComponent<SlotInfo>().LinkedSlots.rightSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Stats.Left))
                    {
                        weight += card.GetComponent<CardStatus>().CardData.FlipCountWeight;
                    }
                    if ((slot.GetComponent<SlotInfo>().LinkedSlots.bottomSlot != null) &&
                        (slot.GetComponent<SlotInfo>().LinkedSlots.bottomSlot.GetComponent<SlotInfo>().OccupiedCard != null) &&
                        (card.GetComponent<CardStatus>().Stats.Bottom >
                        slot.GetComponent<SlotInfo>().LinkedSlots.bottomSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Stats.Top))
                    {
                        weight += card.GetComponent<CardStatus>().CardData.FlipCountWeight;
                    }
//--------------------------------- Early Turn 패널티 계산 ---------------------------------------
                    if(card.GetComponent<CardStatus>().CardData.EarlyTurnStandard < BattleManager.Instance.Round)
                    {
                        weight -= card.GetComponent<CardStatus>().CardData.EarlyTurnPenalty; // Increase weight if the card is suitable for the current round
                    }
//------------------------------- 주변이 막혀있는가 가중치 계산 ---------------------------------
                    if((slot.GetComponent<SlotInfo>().LinkedSlots.leftSlot == null) ||
                        (slot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>().OccupiedCard != null))
                    {
                        weight += (card.GetComponent<CardStatus>().CardData.SurroundingCardBase - card.GetComponent<CardStatus>().Stats.Left); // Increase weight based on surrounding cards
                    }
                    if ((slot.GetComponent<SlotInfo>().LinkedSlots.topSlot == null) ||
                        (slot.GetComponent<SlotInfo>().LinkedSlots.topSlot.GetComponent<SlotInfo>().OccupiedCard != null))
                    {
                        weight += (card.GetComponent<CardStatus>().CardData.SurroundingCardBase - card.GetComponent<CardStatus>().Stats.Top);
                    }
                    if ((slot.GetComponent<SlotInfo>().LinkedSlots.rightSlot == null) ||
                        (slot.GetComponent<SlotInfo>().LinkedSlots.rightSlot.GetComponent<SlotInfo>().OccupiedCard != null))
                    {
                        weight += (card.GetComponent<CardStatus>().CardData.SurroundingCardBase - card.GetComponent<CardStatus>().Stats.Right);
                    }
                    if ((slot.GetComponent<SlotInfo>().LinkedSlots.bottomSlot == null) ||
                        (slot.GetComponent<SlotInfo>().LinkedSlots.bottomSlot.GetComponent<SlotInfo>().OccupiedCard != null))
                    {
                        weight += (card.GetComponent<CardStatus>().CardData.SurroundingCardBase - card.GetComponent<CardStatus>().Stats.Bottom);
                    }

                    if (maxWeight < weight)
                    {
                        maxWeight = weight;
                        maxWeightSlot = slot; // Update the slot with the maximum weight

                    }
                }

                EnemyHandCards[index] = new CardWeight
                {
                    CurrentCard = card,
                    MaxWeightSlot = maxWeightSlot,
                    Weight = maxWeight
                }; // Update the card weight with the maximum weight slot
            }

            int maxWeightIndex = EnemyHandCards.FindIndex(card => card.Weight == EnemyHandCards.Max(c => c.Weight));

            selectedCard = EnemyHandCards[maxWeightIndex].CurrentCard; // Select the card with the maximum weight
            selectedSlot = EnemyHandCards[maxWeightIndex].MaxWeightSlot; // Select the slot with the maximum weight
        }


        if (selectedCard.GetComponent<CardStatus>().CurrentSlot.GetComponent<SlotInfo>().OccupiedCard != null)
        {
            selectedCard.GetComponent<CardStatus>().CurrentSlot.GetComponent<SlotInfo>().OccupiedCard = null; // Clear the current slot of the card
        }

        selectedCard.transform.position = selectedSlot.transform.position;
        selectedCard.GetComponent<SetPositionCard>().SetDropCard();
        selectedCard.GetComponent<Card>().CompareWithAdjacentCards(); // Compare with adjacent cards after placing
        BattleManager.Instance.EndEnemyTurn(); // End enemy turn after placing the card
    }
}
