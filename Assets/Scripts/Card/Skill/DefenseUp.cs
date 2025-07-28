using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUp : MonoBehaviour
{
    private bool isField = false;
    private bool isApplied = false;

    private bool hasLeft = false;
    private bool hasTop = false;
    private bool hasRight = false;
    private bool hasBottom = false;
    private void Awake()
    {
        isField = false;
        isApplied = false;

        hasLeft = false;
        hasTop = false;
        hasRight = false;
        hasBottom = false;
    }
    public void StartDefenseUp(CardStatus cardStatus, int defenseAmount)
    {
        Debug.Log($"DefenseUp Start: {cardStatus.name} - {defenseAmount}");
        Debug.Log(isField);
        Debug.Log(cardStatus.CurrentSlot.layer == LayerMask.NameToLayer("FieldCardSlot"));
        if (!isField && (cardStatus.CurrentSlot.layer == LayerMask.NameToLayer("FieldCardSlot")))
        {
            Debug.Log($"DefenseUp isField: {cardStatus.name} - {defenseAmount}");
            isField = true;
            SlotInfo currentSlotInfo = null;
            SlotInfo leftSlotInfo = null;
            SlotInfo topSlotInfo = null;
            SlotInfo rightSlotInfo = null;
            SlotInfo bottomSlotInfo = null;

            if (cardStatus.CurrentSlot != null)
            {

                currentSlotInfo = cardStatus.CurrentSlot.GetComponent<SlotInfo>();

                if (currentSlotInfo != null)
                {
                    if (currentSlotInfo.LinkedSlots.leftSlot != null)
                    {
                        leftSlotInfo = currentSlotInfo.LinkedSlots.leftSlot.GetComponent<SlotInfo>();
                    }

                    if (currentSlotInfo.LinkedSlots.topSlot != null)
                    {
                        topSlotInfo = currentSlotInfo.LinkedSlots.topSlot.GetComponent<SlotInfo>();
                    }

                    if (currentSlotInfo.LinkedSlots.rightSlot != null)
                    {
                        rightSlotInfo = currentSlotInfo.LinkedSlots.rightSlot.GetComponent<SlotInfo>();
                    }

                    if (currentSlotInfo.LinkedSlots.bottomSlot != null)
                    {
                        bottomSlotInfo = currentSlotInfo.LinkedSlots.bottomSlot.GetComponent<SlotInfo>();
                    }
                }
            }

            hasLeft = leftSlotInfo != null && leftSlotInfo.OccupiedCard != null;
            hasTop = topSlotInfo != null && topSlotInfo.OccupiedCard != null;
            hasRight = rightSlotInfo != null && rightSlotInfo.OccupiedCard != null;
            hasBottom = bottomSlotInfo != null && bottomSlotInfo.OccupiedCard != null;
            if (!isApplied)
            {
                ApplyDefenseUp(cardStatus, defenseAmount);
            }
        }
        else if (isField && isApplied)
        {
            Debug.Log($"DefenseUp isField && !isApplied: {cardStatus.name} - {defenseAmount}");
            ApplyDefenseUp(cardStatus, defenseAmount);
        }

        if (isField && isApplied)
        {
            Debug.Log($"DefenseUp isField && isApplied: {cardStatus.name} - {defenseAmount}");
            var currentSlotInfo = cardStatus.CurrentSlot?.GetComponent<SlotInfo>();

            bool newLeftSlot = false, newTopSlot = false, newRightSlot = false, newBottomSlot = false;

            if (currentSlotInfo.LinkedSlots.leftSlot != null)
                newLeftSlot = currentSlotInfo.LinkedSlots.leftSlot.GetComponent<SlotInfo>()?.OccupiedCard != null;

            if (currentSlotInfo.LinkedSlots.topSlot != null)
                newTopSlot = currentSlotInfo.LinkedSlots.topSlot.GetComponent<SlotInfo>()?.OccupiedCard != null;

            if (currentSlotInfo.LinkedSlots.rightSlot != null)
                newRightSlot = currentSlotInfo.LinkedSlots.rightSlot.GetComponent<SlotInfo>()?.OccupiedCard != null;

            if (currentSlotInfo.LinkedSlots.bottomSlot != null)
                newBottomSlot = currentSlotInfo.LinkedSlots.bottomSlot.GetComponent<SlotInfo>()?.OccupiedCard != null;

            if (hasLeft != newLeftSlot || hasTop != newTopSlot || hasRight != newRightSlot || hasBottom != newBottomSlot)
            {
                ResetDefenseUp(cardStatus, defenseAmount);
            }
        }
    }

    private void ApplyDefenseUp(CardStatus cardStatus, int defenseAmountUp)
    {
        Debug.Log($"DefenseUp Apply: {cardStatus.name} - {defenseAmountUp}");
        cardStatus.Stats.Left += defenseAmountUp;
        cardStatus.Stats.Top += defenseAmountUp;
        cardStatus.Stats.Right += defenseAmountUp;
        cardStatus.Stats.Bottom += defenseAmountUp;

        isApplied = true;
    }

    public void ResetDefenseUp(CardStatus cardStatus, int defenseAmountUp)
    {
        Debug.Log($"DefenseUp Reset: {cardStatus.name} - {defenseAmountUp}");
        cardStatus.Stats.Left -= defenseAmountUp;
        cardStatus.Stats.Top -= defenseAmountUp;
        cardStatus.Stats.Right -= defenseAmountUp;
        cardStatus.Stats.Bottom -= defenseAmountUp;

        isApplied = false;
    }
}
