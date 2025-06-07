using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Card))]
[RequireComponent(typeof(CardStatus))]
public class SetPositionCard : MonoBehaviour
{
    [SerializeField] private LayerMask _cardSlotLayerMask;

    private GameObject _currentSlot;

    private CardStatus _cardStatus;
    private Card _card;
    private void Awake()
    {
        _cardStatus = GetComponent<CardStatus>();
        _card = GetComponent<Card>();
    }
    private void OnEnable()
    {
        if(GetAroundSlot() != null)
        {
            _currentSlot = GetAroundSlot().gameObject;
            _cardStatus.CurrentSlot = _currentSlot;
            SetPositionCardAroundSlot(_currentSlot);
            SetOccupiedCard();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDropCard()
    {
        _currentSlot = GetAroundSlot().gameObject;
        _cardStatus.CurrentSlot = _currentSlot;
        SetPositionCardAroundSlot(_currentSlot);
        SetOccupiedCard();
    }
    public void SetPositionCardAroundSlot(GameObject slot)
    {
        this.transform.position = slot.transform.position;
    }
    public void SetOccupiedCard()
    {
        GameObject card = this.gameObject;
        if (card != null)
        {
            SlotInfo slotInfo = _currentSlot.GetComponent<SlotInfo>();
            if (slotInfo != null)
            {
                slotInfo.OccupiedCard = card;
            }
        }
    }
    private Collider2D GetAroundSlot()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, 1f), 0, _cardSlotLayerMask);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("CardSlot"))
            {
                return collider;
            }
        }
        return null;
    }
}
