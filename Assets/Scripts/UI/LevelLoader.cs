using SimpleAudioManager;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime = 0.1f;
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        SetMusic(scene.name);
    }
    private readonly Dictionary<string, (int songID, int intensity)> _sceneMusic = new Dictionary<string, (int sondID, int intensity)>
    {
        {"Title Scene", (0,0)},
        {"Level Select", (0,1)}
    };
    private void SetMusic(string sceneName)
    {
        if (_sceneMusic.TryGetValue(sceneName, out var setting))
        {
            if(Manager.instance.getSongID() != setting.songID)
                Manager.instance.PlaySong(setting.songID);
            Manager.instance.SetIntensity(setting.intensity);
            Debug.Log("Scene Loaded: " + sceneName + "("+ setting.songID + ", " + setting.intensity+")");
        }
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
