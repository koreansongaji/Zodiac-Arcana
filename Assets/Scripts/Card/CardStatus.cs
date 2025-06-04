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
public class CardStatus : MonoBehaviour
{
    public CardStats Stats;
    [SerializeField] private int _top;
    [SerializeField] private int _bottom;
    [SerializeField] private int _left;
    [SerializeField] private int _right;

    private void Awake()
    {
        Stats.Top = _top;
        Stats.Bottom = _bottom;
        Stats.Left = _left;
        Stats.Right = _right;
    }
}
