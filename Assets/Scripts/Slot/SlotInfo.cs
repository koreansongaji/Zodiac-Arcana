using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct SlotLinks
{
    public GameObject leftSlot;
    public GameObject rightSlot;
    public GameObject topSlot;
    public GameObject bottomSlot;
    public SlotLinks(GameObject left, GameObject right, GameObject top, GameObject bottom)
    {
        leftSlot = left;
        rightSlot = right;
        topSlot = top;
        bottomSlot = bottom;
    }
}
public class SlotInfo : MonoBehaviour
{
    public GameObject OccupiedCard = null;

    [Header("Slot Link")]
    public SlotLinks LinkedSlots;
    [SerializeField] private GameObject _leftSlot;
    [SerializeField] private GameObject _rightSlot;
    [SerializeField] private GameObject _topSlot;
    [SerializeField] private GameObject _bottomSlot;

    private void Awake()
    {
        LinkedSlots = new SlotLinks(_leftSlot, _rightSlot, _topSlot, _bottomSlot);
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
