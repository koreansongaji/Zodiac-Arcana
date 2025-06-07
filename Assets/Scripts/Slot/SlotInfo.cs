using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct SlotLinks
{
    public GameObject leftSlot;
    public GameObject rightSlot;
    public GameObject upSlot;
    public GameObject downSlot;
    public SlotLinks(GameObject left, GameObject right, GameObject up, GameObject down)
    {
        leftSlot = left;
        rightSlot = right;
        upSlot = up;
        downSlot = down;
    }
}
public class SlotInfo : MonoBehaviour
{
    public GameObject OccupiedCard = null;

    [Header("Slot Link")]
    public SlotLinks LinkedSlots;
    [SerializeField] private GameObject _leftSlot;
    [SerializeField] private GameObject _rightSlot;
    [SerializeField] private GameObject _upSlot;
    [SerializeField] private GameObject _downSlot;

    private void Awake()
    {
        LinkedSlots = new SlotLinks(_leftSlot, _rightSlot, _upSlot, _downSlot);
    }
    private void OnEnable()
    {
        if (gameObject.layer == LayerMask.NameToLayer("PlayerCardSlot"))
        {
            BattleManager.Instance.playerSlots.Add(gameObject);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemyCardSlot"))
        {
            BattleManager.Instance.enemySlots.Add(gameObject);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("FieldCardSlot"))
        {
            BattleManager.Instance.FieldSlots.Add(gameObject);
        }
    }
    private void OnDisable()
    {
        if (BattleManager.Instance == null) return;

        if (gameObject.layer == LayerMask.NameToLayer("PlayerCardSlot"))
        {
            BattleManager.Instance.playerSlots.Remove(gameObject);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemyCardSlot"))
        {
            BattleManager.Instance.enemySlots.Remove(gameObject);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("FieldCardSlot"))
        {
            BattleManager.Instance.FieldSlots.Remove(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
