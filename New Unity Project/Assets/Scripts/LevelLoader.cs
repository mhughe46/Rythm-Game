using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    LevelGenerator _levelGenny;

    [SerializeField]
    Settings _settings;

    [SerializeField]
    LevelInitializer _levelInit;
    // Start is called before the first frame update
    void Awake()
    {
        _levelInit = FindObjectOfType<LevelInitializer>();

        _settings.song = _levelInit._selectedLevel.Song;
        _settings.NoteSpawnDelay = SetLevelSpeed(_levelInit._selectedLevel.Difficulty);
        _levelGenny._levelSpeed = SetLevelSpeed(_levelInit._selectedLevel.Difficulty);
        GameObject.FindGameObjectWithTag("Processor").GetComponent<AudioSource>().clip = _levelInit._selectedLevel.Song;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float SetLevelSpeed(int difficulty)
    {
        float levelSpeed = 1;
        if (difficulty >= 5)
        {
            return levelSpeed = 1;
        }
        else if (difficulty >= 4)
        {
            return levelSpeed = 1.35f;
        }
        else if (difficulty >= 3)
        {
            return levelSpeed = 1.5f;
        }
        else if (difficulty >= 2)
        {
            return levelSpeed = 2;
        }
        else if (difficulty < 2)
        {
            return levelSpeed = 3;
        }

        return levelSpeed;
    }
}
