using System.Collections;
using System.Collections.Generic;
using SimpleAudioManager;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void StartSpecific(int num)
    {
        Manager.instance.PlaySong(num);
    }
    public void StartRandom() => Manager.instance.PlaySong(Random.Range(0, 3));

    public void Test() => Manager.instance.PlaySong(new Manager.PlaySongOptions(){
        song = 0,
        intensity = 1,
        startTime = 35.5f
        
    });
    
    public void SetIntensity(int pI) => Manager.instance.SetIntensity(pI);

    public void SetRandomIntensity() => Manager.instance.SetIntensity(Random.Range(0, 3));

    public void StopSong(float pVal) => Manager.instance.StopSong(pVal);
}
