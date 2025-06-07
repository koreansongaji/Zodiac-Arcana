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
    /// ��ó ī��� ���ϴ� �޼����Դϴ�.
    /// </summary>
    public void CompareWithAdjacentCards()
    {   if(_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null 
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Right != null)
        {
            Debug.Log("���� ������ �����մϴ�.");
            if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Owner
                != _cardStatus.Owner)
            {
                Debug.Log("���� ������ ī��� ���մϴ�.");
                if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>().Stats.Right
                    < _cardStatus.Stats.Left)
                {
                    Debug.Log("���� ī�尡 ���� ī�庸�� �۽��ϴ�. ���� ī�带 �������ϴ�.");
                    _cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<Card>().FlipCard();
                }
            }
        }

        if (_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Left != null)
        {
            Debug.Log("������ ������ �����մϴ�.");
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
            Debug.Log("���� ������ �����մϴ�.");
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
            Debug.Log("�Ʒ��� ������ �����մϴ�.");
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

        // ���� ī�� �� ���� ����
        // ��: ���� ī��� ������ ī���� ���¸� ���Ͽ� ���� ���� ó��
        Debug.Log("���� ī��� �� ���Դϴ�.");
    }
    /// <summary>
    /// ī�带 ������ �޼����Դϴ�. ī���� ���¸� �����ϰ� �ִϸ��̼��� �����մϴ�.
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
        // ī�� �ø� ���� ����
        // ��: ī���� ȸ�� �ִϸ��̼�, ���� ���� ��
        Debug.Log("ī�尡 ���������ϴ�.");
    }
}
