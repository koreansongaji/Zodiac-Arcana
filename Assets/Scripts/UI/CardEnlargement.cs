using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class CardEnlargement : MonoBehaviour
{
    [SerializeField] private float _scale = 2f;
    [SerializeField] private float _enlargeTime = 0.2f;
    private Material _outlineShader;
    private Vector3 _targetScale;
    private Vector3 _originalScale;
    private Coroutine _scaleCoroutine;

    public void Start()
    {
        _targetScale = new Vector3(_scale, _scale, _scale);
        _originalScale = transform.localScale;
        _outlineShader = GetComponent<Renderer>().sharedMaterial;
        _outlineShader.SetFloat("_Enabled", 0f);
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered");
        _outlineShader.SetFloat("_Enabled", 1f);
        if (_scaleCoroutine != null)
            StopCoroutine(_scaleCoroutine);
        _scaleCoroutine = StartCoroutine(ScaleTo(_targetScale));

 
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited");
        _outlineShader.SetFloat("_Enabled", 0f);
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
