using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShowCardStatus : MonoBehaviour
{
    [SerializeField] CardStatus _cardStatus;
    [SerializeField] TextMeshProUGUI _upValue;
    [SerializeField] TextMeshProUGUI _downValue;
    [SerializeField] TextMeshProUGUI _leftValue;
    [SerializeField] TextMeshProUGUI _rightValue;
    [SerializeField] TextMeshProUGUI _cardType;

    // Start is called before the first frame update
    void Start()
    {
        UpdateShowStatuse();
    }
    private void OnEnable()
    {
        UpdateShowStatuse();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateShowStatuse()
    {
        if (_upValue != null)
        {
            _upValue.text = _cardStatus.Stats.Top.ToString();
        }
        if (_downValue != null)
        {
            _downValue.text = _cardStatus.Stats.Bottom.ToString();
        }
        if (_leftValue != null)
        {
            _leftValue.text = _cardStatus.Stats.Left.ToString();
        }
        if (_rightValue != null)
        {
            _rightValue.text = _cardStatus.Stats.Right.ToString();
        }
        if (_cardType != null)
        {
            _cardType.text = _cardStatus.CardType.ToString();
        }
    }
}
