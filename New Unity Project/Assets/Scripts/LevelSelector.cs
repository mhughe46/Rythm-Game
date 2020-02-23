using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    Text _levelName;

    [SerializeField]
    Image _thumbnail;

    [SerializeField]
    Image _difficultyImage;

    public Level level;
    // Start is called before the first frame update
    void Start()
    {
        SetDifficultyColor();
        _levelName.text = level.Name;
    }

    public void SelectThisLevel()
    {
        FindObjectOfType<LevelInitializer>()._selectedLevel = level;
    }

    private void SetDifficultyColor()
    {
        if (level.Difficulty >= 5)
        {
            _difficultyImage.color = Color.black;
        }
        else if (level.Difficulty >= 4)
        {
            _difficultyImage.color = Color.red;
        }
        else if (level.Difficulty >= 3)
        {
            _difficultyImage.color = Color.magenta;
        }
        else if (level.Difficulty >= 2)
        {
            _difficultyImage.color = Color.yellow;
        }
        else if (level.Difficulty < 2)
        {
            _difficultyImage.color = Color.green;
        }
    }
}
