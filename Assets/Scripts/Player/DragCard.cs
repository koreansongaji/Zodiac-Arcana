//using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragCard : MonoBehaviour
{
    public GameObject CurSelectedCard;
    private GameObject _selectedCardOriginalSlot;

    private float _mouseClickCoolTime = 0.3f; // Cooldown time for mouse click
    private float _mouseClickCoolTimer = 0f; // Timer to track cooldown
    private bool _isMouseClickCoolDown = false;
    private Coroutine _mouseClickCoolDownCoroutine;

    [SerializeField] private LayerMask _cardLayerMask;
    [SerializeField] private LayerMask _pickUpCardSlotLayerMask; // Layer mask for card slots where cards can be picked up
    [SerializeField] private LayerMask _dropDownCardSlotLayerMask;

    [SerializeField] private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !_isMouseClickCoolDown
            && (BattleManager.Instance.CurrentTurn == TurnState.PlayerTurn)) //좌클릭
        {
            if (CurSelectedCard != null) //내려 놓기
            {
                Vector2 point = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray2D = new Ray2D(point, _mainCamera.transform.forward);

                float distance = Mathf.Infinity;

                RaycastHit2D hit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction, distance, _dropDownCardSlotLayerMask);

                if (hit2D)
                {
                    DropHitCardToSlot(hit2D.transform.gameObject);
                }
            }
            else //카드 들기
            {
                Vector2 point = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray2D = new Ray2D(point, _mainCamera.transform.forward);

                float distance = Mathf.Infinity;

                RaycastHit2D hit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction, distance, _cardLayerMask);

                if (hit2D)
                {
                    SelectHitCardToSlot(hit2D.transform.gameObject);
                }
            }
        }
        if((Input.GetMouseButtonDown(1) && CurSelectedCard != null) 
            || (BattleManager.Instance.PlayerTime <= 1)) //우클릭
        {
            if(CurSelectedCard != null)
            {
                // Right-click to cancel selection and return card to original position
                CurSelectedCard.transform.position = _selectedCardOriginalSlot.transform.position;
                CurSelectedCard.GetComponent<SetPositionCard>().SetSortingLayer(); // Set sorting layer after placing the card
                CurSelectedCard = null;
            }
            CurSelectedCard = null;

            StartMouseCoolTimer();
        }

        ChaseCardToMouse();
    }
    private void ChaseCardToMouse()
    {
        if (CurSelectedCard != null)
        {
            Vector2 point = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            CurSelectedCard.transform.position = point;
        }
    }
    private void SelectHitCardToSlot(GameObject hitCard)
    {
        if (hitCard == null || !hitCard.CompareTag("Card"))
        {
            return; // Ensure the hit object is a card
        }
        //비트, 시프트 연산자 사용했는데 잘 모르겠음;;
        if (((1 << hitCard.GetComponent<CardStatus>().CurrentSlot.gameObject.layer) & _pickUpCardSlotLayerMask) != 0) 
        {
            CurSelectedCard = hitCard;
            CurSelectedCard.GetComponent<SetPositionCard>().SetSortingLayer(); // Set sorting layer after placing the card
            _selectedCardOriginalSlot = CurSelectedCard.GetComponent<CardStatus>().CurrentSlot;
        }
        StartMouseCoolTimer();
    }

    private void DropHitCardToSlot(GameObject hitSlot)
    {
        if (hitSlot != null && hitSlot.GetComponent<SlotInfo>().OccupiedCard == null)
        {
            _selectedCardOriginalSlot.GetComponent<SlotInfo>().OccupiedCard = null; // Clear the original slot
            CurSelectedCard.transform.position = hitSlot.transform.position;
            CurSelectedCard.GetComponent<SetPositionCard>().SetDropCard();
            CurSelectedCard.GetComponent<Card>().CompareWithAdjacentCards(); // Compare with adjacent cards after placing
            CurSelectedCard.GetComponent<SetPositionCard>().SetSortingLayer(); // Set sorting layer after placing the card
            CurSelectedCard = null;
            BattleManager.Instance.EndPlayerTurn(); // End player turn after placing the card
        }
        StartMouseCoolTimer();
    }

    private void StartMouseCoolTimer()
    {
        if (_mouseClickCoolDownCoroutine != null)
        {
            StopCoroutine(_mouseClickCoolDownCoroutine);
            _mouseClickCoolDownCoroutine = null;
        }
        _mouseClickCoolDownCoroutine = StartCoroutine(MouseClickCoolDown());
    }
    IEnumerator MouseClickCoolDown()
    {
        _mouseClickCoolTimer = _mouseClickCoolTime;
        _isMouseClickCoolDown = true;

        while (_mouseClickCoolTimer > 0)
        {
            _mouseClickCoolTimer -= Time.deltaTime;
            yield return null;
        }
        _mouseClickCoolTimer = 0f;
        _isMouseClickCoolDown = false;
    }
}
