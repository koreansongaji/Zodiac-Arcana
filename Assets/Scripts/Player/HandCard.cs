using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    [SerializeField] private List<GameObject> _handSlots = new List<GameObject>();
    [SerializeField] private List<GameObject> _waitingSlots = new List<GameObject>();
    [SerializeField] private List<GameObject> _cards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        GameManager.Instance.GetShuffleList(_cards); // 플레이어 카드 섞기

        foreach (var slot in _handSlots)
        {
            slot.GetComponent<SlotInfo>().OccupiedCard = null;
        }
        foreach (var waitingSlot in _waitingSlots)
        {
            waitingSlot.GetComponent<SlotInfo>().OccupiedCard = null;
        }

        int cardIndex = 0;
        foreach (var slot in _handSlots)
        {
            if (cardIndex < _cards.Count)
            {
                GameObject card = _cards[cardIndex];
                card.transform.position = slot.transform.position; // 카드 위치 설정
                slot.GetComponent<SlotInfo>().OccupiedCard = card; // 슬롯에 카드 할당
                card.GetComponent<SetPositionCard>().SetDropCard(); // 카드의 위치와 상태 설정
                //Debug.Log($"Card {card.name} assigned to slot {slot.name} at position {slot.transform.position}");
                cardIndex++;
            }
            else
            {
                break; // 카드가 더 이상 없으면 종료
            }
        }
        foreach (var waitingSlot in _waitingSlots)
        {
            if (cardIndex < _cards.Count)
            {
                GameObject card = _cards[cardIndex];
                card.transform.position = waitingSlot.transform.position; // 카드 위치 설정
                waitingSlot.GetComponent<SlotInfo>().OccupiedCard = card; // 슬롯에 카드 할당
                card.GetComponent<SetPositionCard>().SetDropCard(); // 카드의 위치와 상태 설정
                //Debug.Log($"Card {card.name} assigned to slot {waitingSlot.name} at position {waitingSlot.transform.position}");
                cardIndex++;
            }
            else
            {
                break; // 카드가 더 이상 없으면 종료
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(BattleManager.Instance.CurrentTurn == TurnState.PlayerTurn)
        {
            foreach (var slot in _handSlots)
            {
                if (slot.GetComponent<SlotInfo>().OccupiedCard == null)
                {
                    foreach (var waitingSlot in _waitingSlots)
                    {
                        if (waitingSlot.GetComponent<SlotInfo>().OccupiedCard != null)
                        {
                            GameObject card = waitingSlot.GetComponent<SlotInfo>().OccupiedCard;
                            waitingSlot.GetComponent<SlotInfo>().OccupiedCard = null; // Clear the occupied card in the behind slot
                            card.transform.position = slot.transform.position; // Move the card to the new slot position
                            card.GetComponent<SetPositionCard>().SetDropCard(); // Set the card's position and state
                            break;
                        }
                    }
                }
            }

        }
    }
}
