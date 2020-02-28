using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

public class LevelInitializer : MonoBehaviour
{
    public List<Level> _includedLevels = new List<Level>();

    public List<Level> _moddedLevels = new List<Level>();

    public Level _selectedLevel;

    public string songPath;

    [SerializeField]
    Texture2D _fallbackThumbnail = null;

    [SerializeField]
    List<AudioClip> _moddedSongs = new List<AudioClip>();

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
        

        songPath = Application.dataPath + "/Resources/Custom Songs";

        if(!Directory.Exists(songPath))
        {
            Directory.CreateDirectory(songPath);
        }

        try
        {
            var songFiles = Directory.EnumerateFiles(songPath, "*.wav");
            

            foreach (string currentFile in songFiles)
            {
                string fileName = currentFile.Substring(songPath.Length + 1);
                //print(fileName);
                StartCoroutine(GetAudioFromFile(songPath, fileName));
            }
        }
        catch
        {
            Debug.LogError("Directory Issues");
        }

        foreach (Level level in _moddedLevels)
        {
            int bpmOutput = _BPMProcessor.calculateBPM(level.Song);
            level.SetDifficultyFromBPM(bpmOutput);
        }

        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = "file://" + path + "/" + filename;
        Debug.Log("Loading from " + audioToLoad);
        using (var request = UnityWebRequestMultimedia.GetAudioClip(audioToLoad, AudioType.WAV))
        {
            yield return request.SendWebRequest();

            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
            clip.name = filename;
            _moddedSongs.Add(clip);

            filename = filename.Replace(".wav", "");
            string levelDetailsPath = path + "/" + filename + ".txt";

            string levelName = filename;
            int levelDifficulty = 1;
            LevelTheme levelTheme = LevelTheme.park;

            if (File.Exists(levelDetailsPath))
            {
                StreamReader reader = new StreamReader(levelDetailsPath);
                while(!reader.EndOfStream)
                {
                    string readLine = reader.ReadLine();
                    if (readLine.ToLower().Contains("name:"))
                    {
                        string output = readLine.Replace("Name:", "");

                        output = output.Trim();

                        levelName = output;
                    }

                    if (readLine.ToLower().Contains("difficulty:"))
                    {
                        string output = readLine.Replace("Difficulty:", "");

                        output = output.Trim();

                        int.TryParse(output, out levelDifficulty);
                    }

                    if (readLine.ToLower().Contains("theme:"))
                    {
                        string output = readLine.ToLower().Replace("theme:", "");

                        output = output.Trim();

                        switch (output)
                        {
                            case "debug":
                                {
                                    levelTheme = LevelTheme.debug;
                                    break;
                                }
                            case "generic":
                                {
                                    levelTheme = LevelTheme.generic;
                                    break;
                                }
                            case "park":
                                {
                                    levelTheme = LevelTheme.park;
                                    break;
                                }
                            case "night":
                                {
                                    levelTheme = LevelTheme.night;
                                    break;
                                }
                            default:
                                {
                                    levelTheme = LevelTheme.park;
                                }
                                break;
                        }
                    }
                }
                reader.Close();
            }

            Level moddedLevel = new Level(levelName, clip, levelDifficulty, levelTheme, _fallbackThumbnail);
            _moddedLevels.Add(moddedLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
