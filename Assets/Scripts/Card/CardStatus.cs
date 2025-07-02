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
    AttackUp
}
[RequireComponent(typeof(Card))]
[RequireComponent(typeof(SetPositionCard))]
public class CardStatus : MonoBehaviour
{
    [SerializeField] private ShowCardStatus _showCardStatus;

    private Card _card;
    private SetPositionCard _setPositionCard;

    public CardStats Stats;
    [SerializeField] private int _top;
    [SerializeField] private int _bottom;
    [SerializeField] private int _left;
    [SerializeField] private int _right;

    public CardType CardType = CardType.Normal;

    public CardOwner Owner;

    public GameObject CurrentSlot;
    private void Awake()
    {
        Stats.Top = _top;
        Stats.Bottom = _bottom;
        Stats.Left = _left;
        Stats.Right = _right;

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
