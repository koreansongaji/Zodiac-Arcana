using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _hoverSoundClip;
    [SerializeField] private AudioClip _clickSoundClip;
    [SerializeField] private AudioClip _backSoundClip;
    [SerializeField] private AudioClip _startSoundClip;
    [SerializeField] private AudioClip _soundBarAdjustSoundClip;
    [SerializeField] private LevelLoader _levelLoader;

    private int _levelsCleared = 0;

    private void Start()
    {
        RefreshGameData();
    }

    public void RefreshGameData()
    {
        GameManager.Instance.SaveDataLoad();
        _levelsCleared = GameManager.Instance.StageData.Stage;
    }
    public void NewGameCheck()
    {
        if(_levelsCleared == 0)
        {
            _levelLoader.LoadSetLevel(7);
        }
        else
        {
            _levelLoader.LoadSetLevel(1);
        }
    }
    public void MouseHover()
    {
        SFXManager.instance.PlaySFXClip(_hoverSoundClip, transform, 1f);

    }
    public void MouseClick()
    {
        SFXManager.instance.PlaySFXClip(_clickSoundClip, transform, 1f);
    }
    public void SoundBarClick()
    {
        SFXManager.instance.PlaySFXClip(_soundBarAdjustSoundClip, transform, 1f);
    }
    public void BackClick()
    {
        SFXManager.instance.PlaySFXClip(_backSoundClip, transform, 1f);
    }
    public void StartClick()
    {
        SFXManager.instance.PlaySFXClip(_startSoundClip, transform, 1f);
    }
}
