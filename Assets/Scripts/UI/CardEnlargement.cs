using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class CardEnlargement : MonoBehaviour
{
    [SerializeField] private float _scale = 1.1f;
    [SerializeField] private float _enlargeTime = 0.2f;
    private Renderer _outlineShader;
    private Vector3 _targetScale;
    private Vector3 _originalScale;
    private Coroutine _scaleCoroutine;

    public void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale * _scale;
        _outlineShader = GetComponent<Renderer>();
        _outlineShader.material = new Material(_outlineShader.sharedMaterial);
        _outlineShader.material.SetFloat("_Enabled", 0f);
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered");
        _outlineShader.material.SetFloat("_Enabled", 1f);
        if (_scaleCoroutine != null)
            StopCoroutine(_scaleCoroutine);
        _scaleCoroutine = StartCoroutine(ScaleTo(_targetScale));

 
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited");
        _outlineShader.material.SetFloat("_Enabled", 0f);
        if (_scaleCoroutine != null)
            StopCoroutine(_scaleCoroutine);
        _scaleCoroutine = StartCoroutine(ScaleTo(_originalScale));


    }

    private IEnumerator ScaleTo(Vector3 target)
    {
        float time = 0;
        while(time < _enlargeTime)
        {
            transform.localScale = Vector3.Lerp(_originalScale, target, time / _enlargeTime);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = target;
    } 

}
