using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragCard : MonoBehaviour
{
    public GameObject CurSelectedCard;
    private Transform SelectedCardOriginalPosition;

    private float MouseClickCoolTime = 0.3f; // Cooldown time for mouse click
    private float MouseClickCoolTimer = 0f; // Timer to track cooldown
    private bool IsMouseClickCoolDown = false;
    private Coroutine MouseClickCoolDownCoroutine;

    [SerializeField] private LayerMask CardLayerMask;
    [SerializeField] private LayerMask CardSlotLayerMask;
    [SerializeField] private Camera MainCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && !IsMouseClickCoolDown)
        {
            if (CurSelectedCard != null)
            {
                Vector2 point = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray2D = new Ray2D(point, MainCamera.transform.forward);

                float distance = Mathf.Infinity;

                RaycastHit2D hit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction, distance, CardSlotLayerMask);

                if (hit2D)
                {
                    DropHitCardToSlot(hit2D.transform.gameObject);
                }
            }
            else
            {
                Vector2 point = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray2D = new Ray2D(point, MainCamera.transform.forward);

                float distance = Mathf.Infinity;

                RaycastHit2D hit2D = Physics2D.Raycast(ray2D.origin, ray2D.direction, distance, CardLayerMask);

                if (hit2D)
                {
                    SelectHitCardToSlot(hit2D.transform.gameObject);
                }
            }
        }
        ChaseCardToMouse();
    }
    private void ChaseCardToMouse()
    {
        if (CurSelectedCard != null)
        {
            Vector2 point = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            CurSelectedCard.transform.position = point;
        }
    }
    private void SelectHitCardToSlot(GameObject hitCard)
    {
        if (hitCard != null)
        {
            CurSelectedCard = hitCard;
            SelectedCardOriginalPosition = CurSelectedCard.transform;
        }
        StartMouseCoolTimer();
    }

    private void DropHitCardToSlot(GameObject hitSlot)
    {
        if (hitSlot != null)
        {
            CurSelectedCard.transform.position = hitSlot.transform.position;
            CurSelectedCard = null;
        }
        StartMouseCoolTimer();
    }

    private void StartMouseCoolTimer()
    {
        if (MouseClickCoolDownCoroutine != null)
        {
            StopCoroutine(MouseClickCoolDownCoroutine);
            MouseClickCoolDownCoroutine = null;
        }
        MouseClickCoolDownCoroutine = StartCoroutine(MouseClickCoolDown());
    }
    IEnumerator MouseClickCoolDown()
    {
        MouseClickCoolTimer = MouseClickCoolTime;
        IsMouseClickCoolDown = true;

        while (MouseClickCoolTimer > 0)
        {
            MouseClickCoolTimer -= Time.deltaTime;
            yield return null;
        }
        MouseClickCoolTimer = 0f;
        IsMouseClickCoolDown = false;
    }
}
