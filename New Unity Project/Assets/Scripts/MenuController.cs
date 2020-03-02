using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    LevelInitializer _levelData;

    [SerializeField]
    Transform _levelHolderIncluded;
    [SerializeField]
    Transform _levelHolderModded;

    [SerializeField]
    Transform _levelPrefab;

    [SerializeField]
    Text _highscoreText;

    [SerializeField]
    Button _playLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _levelData = FindObjectOfType<LevelInitializer>();
        PopulateLevelSelectors();
    }


    // Update is called once per frame
    void Update()
    {
        if (_levelData._selectedLevel.Song == null && _playLevelButton.interactable)
        {
            _playLevelButton.interactable = false;
            _playLevelButton.GetComponentInChildren<Text>().text = "Select a Level";
            _highscoreText.text = "";
        }else if (_levelData._selectedLevel.Song != null && !_playLevelButton.interactable)
        {
            _playLevelButton.interactable = true;
            _playLevelButton.GetComponentInChildren<Text>().text = "Play Level";
            _highscoreText.text = "Highscore: " + _levelData._selectedLevel.Score;
        }else if (_levelData._selectedLevel.Song != null && _playLevelButton.interactable)
        {
            _highscoreText.text = "Highscore: " + _levelData._selectedLevel.Score;
        }
    }

    public void OnMenu(int menuPanel)
    {
        animator.SetInteger("Menu", menuPanel);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        switch(_levelData._selectedLevel.Theme)
        {
            case LevelTheme.park:
                {
                    SceneManager.LoadScene("Enviro_Park");
                    break;
                }
            case LevelTheme.night:
                {
                    SceneManager.LoadScene("Enviro_Night");
                    break;
                }
            case LevelTheme.fall:
                {
                    SceneManager.LoadScene("Enviro_Fall");
                    break;
                }
            case LevelTheme.geometry:
                {
                    SceneManager.LoadScene("Enviro_Night");
                    break;
                }
            case LevelTheme.city:
                {
                    SceneManager.LoadScene("Enviro_Night");
                    break;
                }
            default:
                {
                    SceneManager.LoadScene("Enviro_Park");
                    break;
                }
        }
    }

    private void InstantiateLevelSelector(Level level, Transform gridLayout)
    {
        Transform newLevelSelector = Instantiate(_levelPrefab, gridLayout);
        newLevelSelector.GetComponent<LevelSelector>().level = level;
    }

    private void PopulateLevelSelectors()
    {
        foreach (Level level in _levelData._includedLevels)
        {
            InstantiateLevelSelector(level, _levelHolderIncluded);
        }
        foreach (Level level in _levelData._moddedLevels)
        {
            InstantiateLevelSelector(level, _levelHolderModded);
        }
    }

    
}
