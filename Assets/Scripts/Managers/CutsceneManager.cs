using System.Collections;
using System.Collections.Generic;
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
            if (_currentScene < 4)
            {
                SceneDrawer(_currentScene);
                _currentScene++;
                Debug.Log(_currentScene);
            }
            else
            {
                _loadLevel.LoadSetLevel(1);
            }

        }
    }
    private void SceneDrawer(int sceneNumber)
    {
        if (_animateCoroutine != null)
            StopCoroutine(_animateCoroutine);
        _animateCoroutine = StartCoroutine(SceneLoader(_duration, sceneNumber));
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
}
