using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineCreator : MonoBehaviour
{
    private Renderer _lineShader;
    private Coroutine _animateCoroutine;
    private Material _instanceMaterial;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Button _openStage;

    private void Awake()
    {
        _lineShader = GetComponent<Renderer>();
        _instanceMaterial = new Material(_lineShader.sharedMaterial);
        _lineShader.material = _instanceMaterial;
    }

    public void LineDrawer()
    {
        if (_animateCoroutine != null)
            StopCoroutine(_animateCoroutine);
        _animateCoroutine = StartCoroutine(AnimateLine(_duration));
    }
    public void EnableConnector()
    {
        _lineShader.material.SetFloat("_Progress", 0f);
    }
    private IEnumerator AnimateLine(float time)
    {
        float elapsed = 0f; 
        Debug.Log("Run");
        while (elapsed < time)
        {

            float t = elapsed / time;
            float currValue = Mathf.Lerp(0.5f, 0f, t);
            _lineShader.material.SetFloat("_Progress", currValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _lineShader.material.SetFloat("_Progress", 0f);
        _openStage.interactable = true;
        _openStage.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
}
