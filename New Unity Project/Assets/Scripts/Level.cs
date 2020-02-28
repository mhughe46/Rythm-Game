using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField]
    private string _songName;
    [SerializeField]
    private AudioClip _song;
    private int _difficulty;
    [SerializeField]
    private LevelTheme _levelTheme;
    [SerializeField]
    private Sprite _thumbnail;

    public string Name
    {
        get { return _songName;}
    }
    public AudioClip Song
    {
        get { return _song; }
    }
    public float Length
    {
        get { return _song.length; }
    }
    public int Difficulty
    {
        get { return _difficulty; }
    }
    public LevelTheme Theme
    {
        get { return _levelTheme; }
    }
    public Sprite Thumbnail
    {
        get { return _thumbnail; }
    }


    //Construct a level
    public Level (string name, AudioClip audioClip, int difficulty, LevelTheme theme, Texture2D thumbnail)
    {
        _songName = name;
        _song = audioClip;
        _difficulty = difficulty;
        _levelTheme = theme;
        _thumbnail = Sprite.Create(thumbnail, new Rect(0, 0, thumbnail.width, thumbnail.height), new Vector2(0.5f,0.5f));
    }

    public void SetDifficultyFromBPM(int bpm)
    {
        float bpmRatio = bpm / 100;
        if (bpmRatio >= 3)
        {
            _difficulty = 5;
            return;
        }
        else if (bpmRatio >= 2f)
        {
            _difficulty = 4;
            return;
        }
        else if (bpmRatio >= 1.75f)
        {
            _difficulty = 3;
            return;
        }
        else if (bpmRatio >= 1.4f)
        {
            _difficulty = 2;
            return;
        }
        else if (bpmRatio >= 1)
        {
            _difficulty = 1;
            return;
        }
        else if (bpmRatio < 1)
        {
            _difficulty = 0;
            return;
        }
    }
}

public enum LevelTheme
{
    debug,
    generic,
    park,
    night
}
