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

    private List<Path> _pathsEmpty = new List<Path>();
    private List<Path> _pathsJump = new List<Path>();
    private List<Path> _pathsSlide = new List<Path>();
    private List<Path> _pathsRight = new List<Path>();
    private List<Path> _pathsLeft = new List<Path>();

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
    public Level (string name, AudioClip audioClip, int difficulty, LevelTheme theme, List<Path> pathsEmpty, List<Path> pathsJump, List<Path> pathsSlide, List<Path> pathsRight, List<Path> pathsLeft)
    {
        _songName = name;
        _song = audioClip;
        _difficulty = difficulty;
        _levelTheme = theme;

        foreach (Path path in pathsEmpty)
        {
            _pathsEmpty.Add(path);
        }

        foreach (Path path in pathsJump)
        {
            _pathsJump.Add(path);
        }

        foreach (Path path in pathsSlide)
        {
            _pathsSlide.Add(path);
        }

        foreach (Path path in pathsRight)
        {
            _pathsRight.Add(path);
        }

        foreach (Path path in pathsLeft)
        {
            _pathsLeft.Add(path);
        }
    }

    //Get a specified path at a specified index
    public Transform GetPath(int pathListIndex, int pathIndex)
    {
        List<Path> pathToSearch;
        switch (pathListIndex)
        {
            case 0:
                {
                    pathToSearch = _pathsEmpty;
                    break;
                }
            case 1:
                {
                    pathToSearch = _pathsJump;
                    break;
                }
            case 2:
                {
                    pathToSearch = _pathsSlide;
                    break;
                }
            case 3:
                {
                    pathToSearch = _pathsRight;
                    break;
                }
            case 4:
                {
                    pathToSearch = _pathsLeft;
                    break;
                }
            default:
                {
                    pathToSearch = _pathsEmpty;
                    break;
                }
        }

        if (pathIndex > pathToSearch.Count-1)
        {
            pathIndex = pathToSearch.Count - 1;
        }
        return pathToSearch[pathIndex].pathObject;
    }

    //Get a random path from a specified list
    public Transform GetRandomPath(int pathListIndex)
    {
        List<Path> pathToSearch;
        switch (pathListIndex)
        {
            case 0:
                {
                    pathToSearch = _pathsEmpty;
                    break;
                }
            case 1:
                {
                    pathToSearch = _pathsJump;
                    break;
                }
            case 2:
                {
                    pathToSearch = _pathsSlide;
                    break;
                }
            case 3:
                {
                    pathToSearch = _pathsRight;
                    break;
                }
            case 4:
                {
                    pathToSearch = _pathsLeft;
                    break;
                }
            default:
                {
                    pathToSearch = _pathsEmpty;
                    break;
                }
        }
        return pathToSearch[Random.Range(0,pathToSearch.Count-1)].pathObject;
    }

    public void SetDifficultyFromBPM(int bpm)
    {
        float bpmRatio = bpm / 100;
        if (bpmRatio >= 5)
        {
            _difficulty = 5;
            return;
        }
        else if (bpmRatio >= 4)
        {
            _difficulty = 4;
            return;
        }
        else if (bpmRatio >= 3)
        {
            _difficulty = 3;
            return;
        }
        else if (bpmRatio >= 2)
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
    park
}
