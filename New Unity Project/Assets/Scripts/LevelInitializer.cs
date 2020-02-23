using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitializer : MonoBehaviour
{
    public List<Level> _includedLevels;

    public List<Level> _moddedLevels;

    public Level _selectedLevel;

    [SerializeField]
    List<AudioClip> _moddedSongs;

    [SerializeField]
    private BPMProcessor _BPMProcessor;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Level level in _includedLevels)
        {
            int bpmOutput = _BPMProcessor.calculateBPM(level.Song);
            level.SetDifficultyFromBPM(bpmOutput);
        }
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
