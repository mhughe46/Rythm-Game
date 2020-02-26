using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicDelayer : MonoBehaviour
{
    bool isPlaying;
    public int score;
    public int combo = 1;
    float timePlayed = 0;
    AudioClip clip;
    int incAmount = 1;
    public Text scoreText;


    private void Start()
    {
        Invoke("StartMusic", GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>().NoteSpawnDelay);
    }

    void StartMusic() {
        clip = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>().song;
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying) {
            timePlayed += Time.deltaTime;
            IncreaseScore();
        }
    }

    void IncreaseScore() {
        if (timePlayed <= clip.length)
        {
            score += incAmount * combo;
            Debug.Log(incAmount * combo);
        }
        else {
            isPlaying = false;
        }
        scoreText.text = "Score: " + score + "\n" + "Combo: " + combo;
    }

}
