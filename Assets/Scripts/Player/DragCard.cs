using Autodesk.Fbx;
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

        if (Input.GetMouseButton(0) && !_isMouseClickCoolDown 
            && (BattleTurnManager.Instance.currentTurn == BattleTurnManager.TurnState.PlayerTurn))
        {
            if (CurSelectedCard != null)
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
            else
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
        if(Input.GetMouseButtonDown(1) && CurSelectedCard != null)
        {
            // Right-click to cancel selection and return card to original position
            CurSelectedCard.transform.position = _selectedCardOriginalSlot.transform.position;
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
        if (((1 << hitCard.GetComponent<SetPositionCard>().CurrentSlot.gameObject.layer) & _pickUpCardSlotLayerMask) != 0) 
        {
            CurSelectedCard = hitCard;
            _selectedCardOriginalSlot = CurSelectedCard.GetComponent<SetPositionCard>().CurrentSlot;
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
            CurSelectedCard = null;
            BattleTurnManager.Instance.EndPlayerTurn(); // End player turn after placing the card
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
