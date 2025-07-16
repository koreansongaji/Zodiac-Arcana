using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Card))]
[RequireComponent(typeof(CardStatus))]
public class SetPositionCard : MonoBehaviour
{
    [SerializeField] private LayerMask _cardSlotLayerMask;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private SpriteRenderer _enemySpriteRenderer;
    [SerializeField] private Canvas _textCanvas;
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
        SetDropCard();
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
        // 방어적 초기화
        if (_cardStatus == null) _cardStatus = GetComponent<CardStatus>();
        if (_card == null) _card = GetComponent<Card>();

        Collider2D slotCollider = GetAroundSlot();
        if (slotCollider == null)
        {
            Debug.LogWarning($"{name}: 주변에 슬롯이 없음! SetDropCard 실패");
            return;
        }

        _currentSlot = slotCollider.gameObject;
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

    public void SetSortingLayer()
    {
        if (_playerSpriteRenderer != null)
        {
            if (_playerSpriteRenderer.sortingLayerID == SortingLayer.NameToID("Card"))
            {
                _playerSpriteRenderer.sortingLayerID = SortingLayer.NameToID("Selected");
            }
            else if (_playerSpriteRenderer.sortingLayerID == SortingLayer.NameToID("Selected"))
            {
                _playerSpriteRenderer.sortingLayerID = SortingLayer.NameToID("Card");
            }
        }
        if(_enemySpriteRenderer != null)
        {
            if (_enemySpriteRenderer.sortingLayerID == SortingLayer.NameToID("Card"))
            {
                _enemySpriteRenderer.sortingLayerID = SortingLayer.NameToID("Selected");
            }
            else if (_enemySpriteRenderer.sortingLayerID == SortingLayer.NameToID("Selected"))
            {
                _enemySpriteRenderer.sortingLayerID = SortingLayer.NameToID("Card");
            }
            }
        if (_textCanvas != null)
        {
            if (_textCanvas.sortingLayerName == "Card")
            {
                _textCanvas.sortingLayerName = "Selected";
            }
            else if (_textCanvas.sortingLayerName == "Selected")
            {
                _textCanvas.sortingLayerName = "Card";
            }
        }
    }
}
