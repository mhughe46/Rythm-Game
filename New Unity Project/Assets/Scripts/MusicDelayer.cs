using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDelayer : MonoBehaviour
{
    private void Start()
    {
        Invoke("StartMusic", GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>().NoteSpawnDelay);
    }

    void StartMusic() {
        AudioClip clip = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>().song;
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

}
