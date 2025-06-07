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
[RequireComponent(typeof(Card))]
[RequireComponent(typeof(SetPositionCard))]
public class CardStatus : MonoBehaviour
{
    private Card _card;
    private SetPositionCard _setPositionCard;

    public CardStats Stats;
    [SerializeField] private int _top;
    [SerializeField] private int _bottom;
    [SerializeField] private int _left;
    [SerializeField] private int _right;

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
}
