using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipShader : MonoBehaviour
{
    private Renderer _outlineShader;
    private Coroutine _animateCoroutine;
    [SerializeField] private float _duration = 1f;
    void Start()
    {
        _outlineShader = GetComponent<Renderer>();
        _outlineShader.material = new Material(_outlineShader.sharedMaterial);
    }
    public void flipCardPlayer()
    {
        if (_animateCoroutine != null)
            StopCoroutine(_animateCoroutine);
        _animateCoroutine = StartCoroutine(AnimateDisolveCard(1f, -0.5f, _duration));
    }
    public void flipCardEnemy()
    {
        if (_animateCoroutine != null)
            StopCoroutine(_animateCoroutine);
        _animateCoroutine = StartCoroutine(AnimateDisolveCard(-0.5f, 1f, _duration));
    }

    
    private IEnumerator AnimateDisolveCard(float startVal, float endVal, float time)
    {
        float elapsed = 0f;
        /*_disolveShader = GetComponent<Renderer>();
        _disolveShader.material = new Material(_disolveShader.sharedMaterial);
        */
        Debug.Log("Run");
        while (elapsed < time)
        {

            float t = elapsed / time;
            float currValue = Mathf.Lerp(startVal, endVal, t);
            _outlineShader.material.SetFloat("_DissolveAmount", currValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _outlineShader.material.SetFloat("_DissolveAmount", endVal);
    }
}
