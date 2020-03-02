using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    public void UpdateMusicVolume(Slider slider) {
        mixer.SetFloat("MusicVolume", -80f);
        //mixer.SetFloat("MusicVolume", slider.value);
        Debug.Log(slider.value);
    }

    public void UpdateSFXVolume(Slider slider) {
        mixer.SetFloat("SFXVolume", slider.value);
    }

}
