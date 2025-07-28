using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource sfxObject;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //add random 
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    
    public void PlaySFXClipPitchVar(AudioClip audioClip, Transform spawnTransform, float volume, float minPitch, float maxPitch)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.clip = audioClip;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    public void PlaySFXClipDelayed(AudioClip audioClip, Transform spawnTransform, float volume, float delay)
    {
        StartCoroutine(DelayClip(audioClip, spawnTransform, volume, delay));
    }

    private IEnumerator DelayClip(AudioClip audioClip, Transform spawnTransform, float volume, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

}
