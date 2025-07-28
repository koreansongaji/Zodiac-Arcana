using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LevelSelect : MonoBehaviour
{
    [SerializeField] private List<Button> _levelButtons;
    [SerializeField] private List<GameObject> _levelConnector;
    [SerializeField] private AudioClip _stageHoverSoundClip;
    [SerializeField] private AudioClip _stageClickSoundClip;
    [SerializeField] private AudioClip _connectorSoundClip;

    [SerializeField] private float _minPitch = 0.9f;
    [SerializeField] private float _maxPitch = 1.1f;

    private int _levelsCleared = 0;
    void Start()
    {
        GameManager.Instance.SaveDataLoad();
        _levelsCleared = GameManager.Instance.StageData.Stage;
        UnlockStagesSound(_levelsCleared);
    }
    public void MouseHoverSound()
    {
        SFXManager.instance.PlaySFXClipPitchVar(_stageHoverSoundClip, transform, 0.3f, _minPitch, _maxPitch);
    }
    public void MouseClickSound()
    {
        SFXManager.instance.PlaySFXClipPitchVar(_stageClickSoundClip, transform, 1f, _minPitch, _maxPitch);
    }
    private void UnlockStagesSound(int levelsCleared)
    {
        int i, j;
        
        for (i = 0; i < _levelButtons.Count; i++)
        {
            _levelButtons[i].interactable = i <= levelsCleared-1;
            EventTrigger trigger = _levelButtons[i].GetComponent<EventTrigger>();
            trigger.enabled = i <= levelsCleared;
            SpriteRenderer outline = _levelButtons[i].GetComponentInChildren<SpriteRenderer>();
            outline.enabled = i <= levelsCleared-1;
        }
        if(levelsCleared == 0)
        {
            _levelButtons[0].interactable = true;
            SpriteRenderer outline = _levelButtons[0].GetComponentInChildren<SpriteRenderer>();
            outline.enabled = true;
        }
        for(j = 0; j <levelsCleared-1 && j < _levelConnector.Count; j++)
        {
            LineCreator lineDraw = _levelConnector[j].GetComponentInChildren<LineCreator>();
            lineDraw.EnableConnector();
        }
        if(levelsCleared < _levelConnector.Count && levelsCleared != 0)
        {
            LineCreator lineDraw = _levelConnector[j].GetComponentInChildren<LineCreator>();
            lineDraw.LineDrawer();
            SFXManager.instance.PlaySFXClipDelayed(_connectorSoundClip, transform, 0.5f,0f);
        }
       

    }
}
