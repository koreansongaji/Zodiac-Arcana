using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime = 5.0f;

    /*private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //LoadNextLevel();
        //_transition.Play("Crossfade_Start");
       
    }*/


    public void LoadNextLevel() => StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    public void LoadSetLevel(int levelIndex) => StartCoroutine(LoadLevel(levelIndex));
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    private IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log("Starting transition");
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(_transitionTime);
        Debug.Log("Loading Scene");
        SceneManager.LoadScene(levelIndex);
    }
}
