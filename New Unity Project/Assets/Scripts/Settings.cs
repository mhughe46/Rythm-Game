using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public AudioClip song;
    public float NoteSpawnDelay = 1;
    

    private void Update()
    {
        //mixer.SetFloat("MusicVolume", musicVolume);
        //mixer.SetFloat("SFXVolume", sfxVolume);
    }


}
