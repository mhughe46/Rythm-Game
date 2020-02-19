using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private string _songName;
    private AudioClip _song;
    private int _difficulty;
    private LevelTheme _levelTheme;

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

    //Construct a level
    public Level (string name, AudioClip audioClip, LevelTheme theme)
    {
        _songName = name;
        _song = audioClip;
        _difficulty = 1;
        _levelTheme = theme;
    }
}

public enum LevelTheme
{
    debug,
    park
}
