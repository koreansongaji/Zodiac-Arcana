using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("Battle Time")]
    [SerializeField] private Slider _playerTimeGauge;
    [SerializeField] private Slider _enemyTimeGauge;

    [Header("Battle Turn")]
    [SerializeField] private GameObject _turnIndicator;

    [Header("Battle Card Count")]
    [SerializeField] private List<GameObject> _playerCardCount;
    [SerializeField] private float _changeFillAmount = 5f; // 카드 카운트 변경 속도
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        BattleManager.Instance.BattleUI = this; // BattleManager에 BattleUI 할당
        CardCount(); // 초기 카드 카운트 설정
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        _playerTimeGauge.value = (BattleManager.Instance.PlayerTime - 1) / 30.0f;
        _enemyTimeGauge.value = (BattleManager.Instance.EnemyTime - 1) / 30.0f;
    }

    public void CardCount()
    {
        for (int i = 0; i < 9; i++)
        {
            Image image = _playerCardCount[i].GetComponent<Image>();
            float targetAmount = i < BattleManager.Instance.PlayerCards.Count ? 1.0f : 0.0f;
            StartCoroutine(ChangeFillAmount(image, targetAmount));
        }
    }

    private IEnumerator ChangeFillAmount(Image image, float targetAmount)
    {
        float elapsedTime = 0f;
        float maxDuration = 1f; // 최대 대기 시간 1초

        while (Mathf.Abs(image.fillAmount - targetAmount) > 0.01f)
        {
            image.fillAmount = Mathf.MoveTowards(image.fillAmount, targetAmount, _changeFillAmount * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= maxDuration)
            {
                break;
            }

            yield return null;
        }

        image.fillAmount = targetAmount; // 보정
    }
}
