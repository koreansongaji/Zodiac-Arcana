using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardStatus))]
[RequireComponent(typeof(SetPositionCard))]
public class Card : MonoBehaviour
{
    private CardStatus _cardStatus;
    private SetPositionCard _setPositionCard;

    [SerializeField] private GameObject _playerFace;
    [SerializeField] private GameObject _enemyFace;
    private void Awake()
    {
        _cardStatus = GetComponent<CardStatus>();
        _setPositionCard = GetComponent<SetPositionCard>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        if (gameObject.layer == LayerMask.NameToLayer("PlayerCard"))
        {
            if (!BattleManager.Instance.playerCards.Contains(gameObject))
            {
                BattleManager.Instance.playerCards.Add(gameObject);
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemyCard"))
        {
            if (!BattleManager.Instance.enemyCards.Contains(gameObject))
            {
                BattleManager.Instance.enemyCards.Add(gameObject);

            }
        }
    }
    private void OnDisable()
    {
        if (gameObject.layer == LayerMask.NameToLayer("PlayerCard"))
        {
            BattleManager.Instance.playerCards.Remove(gameObject);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemyCard"))
        {
            BattleManager.Instance.enemyCards.Remove(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 근처 카드와 비교하는 메서드입니다.
    /// </summary>
    public void CompareWithAdjacentCards()
    {   if(_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null 
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Right != null)
        {
            Debug.Log("왼쪽 슬롯이 존재합니다.");
            if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Owner
                != _cardStatus.Owner)
            {
                Debug.Log("왼쪽 슬롯의 카드와 비교합니다.");
                if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>().Stats.Right
                    < _cardStatus.Stats.Left)
                {
                    Debug.Log("왼쪽 카드가 현재 카드보다 작습니다. 왼쪽 카드를 뒤집습니다.");
                    _cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<Card>().FlipCard();
                }
            }
        }

        if (_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Left != null)
        {
            Debug.Log("오른쪽 슬롯이 존재합니다.");
            if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.rightSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Owner
                != _cardStatus.Owner)
            {
                if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.rightSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Stats.Left
                    < _cardStatus.Stats.Right)
                {
                    _cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.rightSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<Card>().FlipCard();
                }
            }
        }


        if (_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.upSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.upSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null 
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.upSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Bottom != null)
        {
            Debug.Log("위쪽 슬롯이 존재합니다.");
            if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.upSlot.GetComponent<SlotInfo>().OccupiedCard?.GetComponent<CardStatus>().Owner
                != _cardStatus.Owner)
            {
                if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.upSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Bottom
                    < _cardStatus.Stats.Top)
                {
                    _cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.upSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<Card>().FlipCard();
                }
            }
        }

        if (_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.downSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.downSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.downSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Top != null)
        {
            Debug.Log("아래쪽 슬롯이 존재합니다.");
            if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.downSlot.GetComponent<SlotInfo>().OccupiedCard?.GetComponent<CardStatus>().Owner
                != _cardStatus.Owner)
            {
                if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.downSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>().Stats.Top
                    < _cardStatus.Stats.Bottom)
                {
                    _cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.downSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<Card>().FlipCard();
                }
            }
        }

        // 인접 카드 비교 로직 구현
        // 예: 현재 카드와 인접한 카드의 상태를 비교하여 게임 로직 처리
        Debug.Log("인접 카드와 비교 중입니다.");
    }
    /// <summary>
    /// 카드를 뒤집는 메서드입니다. 카드의 상태를 변경하고 애니메이션을 실행합니다.
    /// </summary>
    public void FlipCard()
    {
        if(_cardStatus.Owner == CardOwner.Player)
        {
            _playerFace.SetActive(false);
            _enemyFace.SetActive(true);
            _cardStatus.Owner = CardOwner.Enemy;
        }
        else if (_cardStatus.Owner == CardOwner.Enemy)
        {
            _playerFace.SetActive(true);
            _enemyFace.SetActive(false);
            _cardStatus.Owner = CardOwner.Player;
        }
        // 카드 플립 로직 구현
        // 예: 카드의 회전 애니메이션, 상태 변경 등
        Debug.Log("카드가 뒤집혔습니다.");
    }
}
