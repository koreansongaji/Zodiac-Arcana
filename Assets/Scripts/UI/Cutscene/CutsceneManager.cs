using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private List <CanvasGroup> _scene;
    [SerializeField] private LevelLoader _loadLevel;
    [SerializeField] private float _duration;
    private Coroutine _animateCoroutine;
    private int _currentScene = 1;
    private int _finished = 0;
    private void Start()
    {

        _scene[0].alpha = 1;
        for (int i = 1; i < _scene.Count; i++)
        {
            _scene[i].alpha = 0;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(_currentScene);
            if (_currentScene < _scene.Count-1)
            {
                SceneDrawer(_currentScene);
            }
            else if(_finished == 1)
            {
                _loadLevel.LoadSetLevel(1);
            }
            _currentScene++;
            
        }
    }
    private void SceneDrawer(int sceneNumber)
    {
        if (sceneNumber < _scene.Count-2)
        {
            if (_animateCoroutine != null)
                StopCoroutine(_animateCoroutine);
            _animateCoroutine = StartCoroutine(SceneLoader(_duration, sceneNumber));
        }
        else
        {
            if (_animateCoroutine != null)
                StopCoroutine(_animateCoroutine);
            _animateCoroutine = StartCoroutine(FinalFrame(_duration, sceneNumber));
        }
    }
    private IEnumerator SceneLoader(float duration, int sceneNumber)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float currValue = Mathf.Lerp(0f, 1f, t);
            _scene[sceneNumber].alpha = currValue;
            elapsed += Time.deltaTime;
            yield return null;
        }
        _scene[sceneNumber].alpha = 1;
    }
    private IEnumerator FinalFrame(float duration, int sceneNumber)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float currValue = Mathf.Lerp(0f, 1f, t);
            _scene[sceneNumber].alpha = currValue;
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        _scene[sceneNumber].alpha = 1;

        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float currValue = Mathf.Lerp(0f, 1f, t);
            _scene[sceneNumber+1].alpha = currValue;
            elapsed += Time.deltaTime;
            yield return null;
        }
        _scene[sceneNumber+1].alpha = 1;
        _finished = 1;
    }
}

