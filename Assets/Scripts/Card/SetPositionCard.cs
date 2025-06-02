using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionCard : MonoBehaviour
{
    [SerializeField] private LayerMask _cardSlotLayerMask;

    public GameObject CurrentSlot;

    private void OnEnable()
    {
        CurrentSlot = GetAroundSlot().gameObject;
        SetPositionCardAroundSlot(CurrentSlot);
        SetOccupiedCard();
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
        CurrentSlot = GetAroundSlot().gameObject;
        SetPositionCardAroundSlot(CurrentSlot);
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
            SlotInfo slotInfo = CurrentSlot.GetComponent<SlotInfo>();
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
