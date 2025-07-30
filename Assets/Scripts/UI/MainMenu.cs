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
    public void ResetFile()
    {
        GameManager.Instance.StageData.Stage = 0; // 스테이지 초기화
        GameManager.Instance.SaveDataSave(); // 저장 데이터 저장
    }
}
