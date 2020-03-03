using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicDelayer : MonoBehaviour
{
    public bool isPlaying;
    public int score;
    public int combo = 1;
    public int bestCombo = 1;
    float timePlayed = 0;
    AudioClip clip;
    int incAmount = 1;
    public Text scoreText;
    public Text winScoreText;
    public GameObject WinPanel;


    private void Start()
    {
        Invoke("StartMusic", GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>().NoteSpawnDelay);
        bestCombo = 0;
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
        bool isDead = FindObjectOfType<PlayerHealth>().isDead;
        if (timePlayed > clip.length && !isDead && !WinPanel.activeSelf) {
            WinPanel.SetActive(true);
            Level levelData = FindObjectOfType<LevelInitializer>()._selectedLevel;
            if(score > levelData.Score)
            {
                levelData.Score = score;
                PlayerPrefs.SetInt(levelData.Name, score);
                print("New score" + score);
                PlayerPrefs.Save();
            }
        }
    }

    void IncreaseScore() {
        if (timePlayed <= clip.length)
        {
            if (!FindObjectOfType<SettingsMenu>().isPaused)
            {
                score += incAmount * combo;
                if (combo > bestCombo)
                {
                    bestCombo = combo;
                }
            }
            //Debug.Log(incAmount * combo);
        }
        else {
            isPlaying = false;
        }
        scoreText.text = "Score: " + score + "\n" + "Combo: " + combo;
        winScoreText.text = "Score: " + score + "\n" + "Best Combo: " + bestCombo;
    }

}
