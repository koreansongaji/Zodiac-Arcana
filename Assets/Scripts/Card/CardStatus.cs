using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct CardStats
{
    public int Top;
    public int Bottom;
    public int Left;
    public int Right;
    public CardStats(int top, int bottom, int left, int right)
    {
        Top = top;
        Bottom = bottom;
        Left = left;
        Right = right;
    }
}
public enum CardOwner
{
    Player,
    Enemy
}
public enum CardType
{
    Normal,
    TeamBuff,
    Counter,
    DefenseUp,
    AttackUp,
    Ability_Nullification //능력 한 번 쓰고 능력이 사라짐
}
[RequireComponent(typeof(Card))]
[RequireComponent(typeof(SetPositionCard))]
public class CardStatus : MonoBehaviour
{
    [SerializeField] public CardData CardData;
    [SerializeField] private ShowCardStatus _showCardStatus;

    private Card _card;
    private SetPositionCard _setPositionCard;

    public CardStats Stats = new CardStats();

    public CardType CardType = CardType.Normal;

    public CardOwner Owner;

    public GameObject CurrentSlot;

    [HideInInspector] public int surroundingCard = 0;
    private void Awake()
    {
        Stats = new CardStats(CardData.Top, CardData.Bottom, CardData.Left, CardData.Right);

        _card = GetComponent<Card>();
        _setPositionCard = GetComponent<SetPositionCard>();
    }
    
    public void ChangeStatus(CardStats buffAmount)
    {
        //Debug.Log($"Changing status for card: {gameObject.name} with buff amount: {buffAmount.Top}, {buffAmount.Bottom}, {buffAmount.Left}, {buffAmount.Right}");
        Stats.Top = CardData.Top + buffAmount.Top;
        Stats.Bottom = CardData.Bottom + buffAmount.Bottom;
        Stats.Left = CardData.Left + buffAmount.Left;
        Stats.Right = CardData.Right + buffAmount.Right;

        if(CardType == CardType.AttackUp)
        {
            if(CurrentSlot.layer == LayerMask.NameToLayer("PlayerCardSlot") 
                || CurrentSlot.layer == LayerMask.NameToLayer("EnemyCardSlot"))
            {
                Stats.Top += _card.Ability_Amount;
                Stats.Bottom += _card.Ability_Amount;
                Stats.Left += _card.Ability_Amount;
                Stats.Right += _card.Ability_Amount;
            }
        }

        // Update the card's visual representation if needed
        // For example, update a UI element or sprite to reflect the new stats
        _showCardStatus?.UpdateShowStatuse();
    }
}
