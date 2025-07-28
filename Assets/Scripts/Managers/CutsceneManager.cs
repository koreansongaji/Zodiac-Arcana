using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private List <CanvasGroup> _scene;
    [SerializeField] private LevelLoader _loadLevel;
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
                _scene[_currentScene].alpha = 1;
                _currentScene++;
                Debug.Log(_currentScene);
            }
            else
            {
                _loadLevel.LoadSetLevel(1);
            }

        }
    }
}
