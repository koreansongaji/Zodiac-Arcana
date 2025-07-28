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
    private CardFlipShader _shaderCard;

    [Header("Skill")]
    private TeamBuff _teamBuff;
    private AttackUp _attackUp;
    private DefenseUp _defenseUp;
    public int Ability_Amount = 1; // 팀 버프 적용량, 카운터 감소량 등
    private void Awake()
    {
        _cardStatus = GetComponent<CardStatus>();
        _setPositionCard = GetComponent<SetPositionCard>();
        TryGetComponent<TeamBuff>(out _teamBuff);
        TryGetComponent<AttackUp>(out _attackUp);
        TryGetComponent<DefenseUp>(out _defenseUp);
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerFace.SetActive(true);
        _enemyFace.SetActive(true);
        _shaderCard = _playerFace.GetComponent<CardFlipShader>();
    if (_cardStatus.Owner == CardOwner.Player)
    {
            _shaderCard.flipCardPlayer();
    }
    else if (_cardStatus.Owner == CardOwner.Enemy)
    {
            _shaderCard.flipCardEnemy();
        }
}
    private void OnEnable()
    {
        AddCardToManager();
    }
    private void AddCardToManager()
    {
        if (gameObject.GetComponent<CardStatus>().Owner == CardOwner.Player)
        {
            if (!BattleManager.Instance.PlayerCards.Contains(gameObject))
            {
                BattleManager.Instance.PlayerCards.Add(gameObject);
            }
            if(BattleManager.Instance.EnemyCards.Contains(gameObject))
            {
                BattleManager.Instance.EnemyCards.Remove(gameObject);
            }
        }
        else if (gameObject.GetComponent<CardStatus>().Owner == CardOwner.Enemy)
        {
            if (!BattleManager.Instance.EnemyCards.Contains(gameObject))
            {
                BattleManager.Instance.EnemyCards.Add(gameObject);
            }
            if (BattleManager.Instance.PlayerCards.Contains(gameObject))
            {
                BattleManager.Instance.PlayerCards.Remove(gameObject);
            }
        }
        if (BattleManager.Instance.BattleUI != null)
        {
            BattleManager.Instance.BattleUI.CardCount();
        }
    }
    private void OnDisable()
    {
        if (gameObject.layer == LayerMask.NameToLayer("PlayerCard"))
        {
            BattleManager.Instance.PlayerCards.Remove(gameObject);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemyCard"))
        {
            BattleManager.Instance.EnemyCards.Remove(gameObject);
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
    {   
        if(_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null 
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.leftSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Right != null)
        {
            //Debug.Log("왼쪽 슬롯이 존재합니다.");
            if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>().OccupiedCard.GetComponent<CardStatus>().Owner
                != _cardStatus.Owner)
            {
                //Debug.Log("왼쪽 슬롯의 카드와 비교합니다.");
                if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>().Stats.Right
                    < _cardStatus.Stats.Left)
                {
                    //Debug.Log("왼쪽 카드가 현재 카드보다 작습니다. 왼쪽 카드를 뒤집습니다.");
                    _cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.leftSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<Card>().FlipCard();
                }
            }
        }

        if (_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.rightSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Left != null)
        {
            //Debug.Log("오른쪽 슬롯이 존재합니다.");
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


        if (_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.topSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.topSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null 
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.topSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Bottom != null)
        {
            //Debug.Log("위쪽 슬롯이 존재합니다.");
            if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.topSlot.GetComponent<SlotInfo>().OccupiedCard?.GetComponent<CardStatus>().Owner
                != _cardStatus.Owner)
            {
                if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.topSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Bottom
                    < _cardStatus.Stats.Top)
                {
                    _cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.topSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<Card>().FlipCard();
                }
            }
        }

        if (_cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.bottomSlot != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.bottomSlot?.GetComponent<SlotInfo>()?.OccupiedCard != null
            && _cardStatus?.CurrentSlot?.GetComponent<SlotInfo>()?.LinkedSlots.bottomSlot?.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>()?.Stats.Top != null)
        {
            //Debug.Log("아래쪽 슬롯이 존재합니다.");
            if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.bottomSlot.GetComponent<SlotInfo>().OccupiedCard?.GetComponent<CardStatus>().Owner
                != _cardStatus.Owner)
            {
                if (_cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.bottomSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<CardStatus>().Stats.Top
                    < _cardStatus.Stats.Bottom)
                {
                    _cardStatus.CurrentSlot.GetComponent<SlotInfo>().LinkedSlots.bottomSlot.GetComponent<SlotInfo>()?.OccupiedCard.GetComponent<Card>().FlipCard();
                }
            }
        }
        UseSkill();
        // 인접 카드 비교 로직 구현
        // 예: 현재 카드와 인접한 카드의 상태를 비교하여 게임 로직 처리
        //Debug.Log("인접 카드와 비교 중입니다.");
    }
    public void UseSkill()
    {
        if(_cardStatus.CardType == CardType.Normal)
        {
            return;
        }
        else if(_cardStatus.CardType == CardType.TeamBuff)
        {
            // 팀 버프 스킬 사용 로직
            //Debug.Log("팀 버프 스킬 사용 중입니다.");
            if (_teamBuff == null)
            {
                TryGetComponent<TeamBuff>(out _teamBuff);
            }
            if (_teamBuff != null)
            {
                if (_cardStatus.CurrentSlot.layer == LayerMask.NameToLayer("FieldCardSlot"))
                {
                    // 플레이어 팀 버프 적용
                    _teamBuff.ApplyBuffToTeam(_cardStatus.Owner, Ability_Amount);
                }
            }
        }

        //else if(_cardStatus.CardType == CardType.Counter)
        //{
        //    // 카운터 스킬 사용 로직
        //    //Debug.Log("카운터 스킬 사용 중입니다.");
        //}
        //else if(_cardStatus.CardType == CardType.Ability_Nullification)
        //{
        //    // 능력 무효화 스킬 사용 로직
        //    //Debug.Log("능력 무효화 스킬 사용 중입니다.");
        //}
    }
    /// <summary>
    /// 카드를 뒤집는 메서드입니다. 카드의 상태를 변경하고 애니메이션을 실행합니다.
    /// </summary>
    public void FlipCard()
    {
        if(_cardStatus.Owner == CardOwner.Player)
        {
            _shaderCard.flipCardEnemy();
            _cardStatus.Owner = CardOwner.Enemy;
        }
        else if (_cardStatus.Owner == CardOwner.Enemy)
        {
            _shaderCard.flipCardPlayer();
            _cardStatus.Owner = CardOwner.Player;
        }
        AddCardToManager();
        // 카드 플립 로직 구현
        // 예: 카드의 회전 애니메이션, 상태 변경 등
        //Debug.Log("카드가 뒤집혔습니다.");
    }
}
