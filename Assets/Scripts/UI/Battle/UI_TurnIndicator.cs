using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TurnIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _playerTurnIndicator;
    [SerializeField] private GameObject _enemyTurnIndicator;
    private Animator _playerTurnAnimator;
    private void Awake()
    {
        TryGetComponent<Animator>(out _playerTurnAnimator);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTurnIndicator(bool isPlayerTurn)
    {
        _playerTurnAnimator.SetBool("isPlayerTurn", isPlayerTurn);
        Debug.Log($"SetTurnIndicator: {isPlayerTurn}");
    }
}
