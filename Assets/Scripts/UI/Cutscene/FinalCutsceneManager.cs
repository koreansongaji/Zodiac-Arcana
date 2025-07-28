using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCutsceneManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _lightBackdrop;
    [SerializeField] private CanvasGroup _text1;
    [SerializeField] private CanvasGroup _text2;
    [SerializeField] private CardFlipShader _whale;
    [SerializeField] private float _duration;
    [SerializeField] private LevelLoader _levelLoader;

    private int _finished;
    private int _sceneNumber = 0;
    private Coroutine _animateCoroutine;
    void Start()
    {
        _lightBackdrop.alpha = 0f;
        _text1.alpha = 0f;
        _text2.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_sceneNumber == 0)
            {
                _whale.flipCardEnemy();
                _sceneNumber++;
                _finished = 1;
            }
            else if(_sceneNumber == 1 && _finished == 1)
            {
                _finished = 0;
                SceneDrawer(_lightBackdrop);
                _sceneNumber++;
            }
            else if(_sceneNumber == 2 && _finished == 1)
            {
                _finished = 0;
                FinalTextDrawer(_text1, _text2);
                _sceneNumber++;
            }
            else if(_finished == 1)
            {
                _levelLoader.LoadSetLevel(0);
            }
            
        }
    }
    private void SceneDrawer(CanvasGroup scene)
    {

        if (_animateCoroutine != null)
            StopCoroutine(_animateCoroutine);
        _animateCoroutine = StartCoroutine(SceneLoader(_duration, scene));
    }
    private void FinalTextDrawer(CanvasGroup first, CanvasGroup second)
    {

        if (_animateCoroutine != null)
            StopCoroutine(_animateCoroutine);
        _animateCoroutine = StartCoroutine(FinalText(first, second));
    }
    private IEnumerator SceneLoader(float duration, CanvasGroup scene)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float currValue = Mathf.Lerp(0f, 1f, t);
            scene.alpha = currValue;
            elapsed += Time.deltaTime;
            yield return null;
        }
        scene.alpha = 1;
        _finished = 1;
    }
    private IEnumerator FinalText (CanvasGroup first, CanvasGroup second)
    {
        float elapsed = 0f;
        while (elapsed < _duration)
        {
            float t = elapsed / _duration;
            float currValue = Mathf.Lerp(0f, 1f, t);
            first.alpha = currValue;
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        first.alpha = 1;

        elapsed = 0f;
        while (elapsed < _duration)
        {
            float t = elapsed / _duration;
            float currValue = Mathf.Lerp(0f, 1f, t);
            second.alpha = currValue;
            elapsed += Time.deltaTime;
            yield return null;
        }
        second.alpha = 1;
        _finished = 1;
    }

}
