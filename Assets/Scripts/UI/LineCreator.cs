using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    private Renderer _lineShader;
    private Coroutine _animateCoroutine;
    [SerializeField] private float _duration = 1f;

    void Start()
    {
        _lineShader = GetComponent<Renderer>();
        _lineShader.material = new Material(_lineShader.sharedMaterial);
    }
    public void LineDrawer()
    {
        if (_animateCoroutine != null)
            StopCoroutine(_animateCoroutine);
        _animateCoroutine = StartCoroutine(AnimateLine(_duration));
    }
    public void setProgress(float progress)
    {
        _lineShader.material.SetFloat("_Progress", progress);
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
    }
}
