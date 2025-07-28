using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private List<Button> _levelButtons;
    [SerializeField] private List<GameObject> _levelConnector;
    [SerializeField] private List<AudioClip> _stageClickSoundClip;
    [SerializeField] private AudioClip _connectorSoundClip;
    private int _levelsCleared = 0;
    void Start()
    {
        _levelsCleared = 3;
        UnlockStages(_levelsCleared);
    }

    private void UnlockStages(int levelsCleared)
    {
        int i, j;
        
        for (i = 0; i < _levelButtons.Count; i++)
        {
            _levelButtons[i].interactable = i <= levelsCleared-1;
            SpriteRenderer outline = _levelButtons[i].GetComponentInChildren<SpriteRenderer>();
            outline.enabled = i <= levelsCleared-1;
        }
        for(j = 0; j <levelsCleared-1 && j < _levelConnector.Count; j++)
        {
            LineCreator lineDraw = _levelConnector[j].GetComponentInChildren<LineCreator>();
            lineDraw.EnableConnector();
        }
        if(levelsCleared < _levelConnector.Count)
        {
            LineCreator lineDraw = _levelConnector[j].GetComponentInChildren<LineCreator>();
            lineDraw.LineDrawer();
            SFXManager.instance.PlaySFXClipDelayed(_connectorSoundClip, transform, 0.5f,0f);
        }
       

    }
}
