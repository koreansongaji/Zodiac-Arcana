using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data", order = int.MaxValue)]
public class CardData : ScriptableObject
{
    [SerializeField]
    private string cardName;
    public string CardName { get { return cardName; } }

    [Header("Card Stats")]
    [SerializeField]
    private int left;
    public int Left { get { return left; } }
    [SerializeField]
    private int top;
    public int Top { get { return top; } }
    [SerializeField]
    private int right;
    public int Right { get { return right; } }
    [SerializeField]
    private int bottom;
    public int Bottom { get { return bottom; } }
    [SerializeField]
    private CardType type;
    public CardType Type { get { return type; } }

    [Header("Card Weight")]
    [SerializeField]
    private int flipCountWeight;
    public int FlipCountWeight { get { return flipCountWeight; } }
    [SerializeField]
    private int earlyTurnStandard;
    public int EarlyTurnStandard { get { return earlyTurnStandard; } }
    [SerializeField]
    private int earlyTurnPenalty;
    public int EarlyTurnPenalty { get { return earlyTurnPenalty; } }
    [SerializeField]
    private int surroundingCardBase;
    public int SurroundingCardBase { get { return surroundingCardBase; } }

}
