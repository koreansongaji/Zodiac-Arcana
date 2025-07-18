using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CardStats
{
    public int Top;
    public int Bottom;
    public int Left;
    public int Right;
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
        Stats.Top = CardData.Top;
        Stats.Bottom = CardData.Bottom;
        Stats.Left = CardData.Left;
        Stats.Right = CardData.Right;

        _card = GetComponent<Card>();
        _setPositionCard = GetComponent<SetPositionCard>();
    }

    public void ChangeStatus(int amount)
    {
        Stats.Top += amount;
        Stats.Bottom += amount;
        Stats.Left += amount;
        Stats.Right += amount;
        // Update the card's visual representation if needed
        // For example, update a UI element or sprite to reflect the new stats
        _showCardStatus?.UpdateShowStatuse();
    }
}
