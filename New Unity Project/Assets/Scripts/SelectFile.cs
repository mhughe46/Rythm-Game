using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SelectFile : MonoBehaviour
{
    AudioSource source;
    private void Start()
    {
        //OpenSelector();
        source = GetComponent<AudioSource>();
    }
    public void OpenSelector() {
        string filePath = EditorUtility.OpenFilePanel("Select Song", "", "wav");
        string newPath = Application.dataPath + "/Resources/Custom Songs/" + System.IO.Path.GetFileName(filePath);
        FileUtil.CopyFileOrDirectory(filePath, newPath);
        SceneManager.LoadScene(0);
    }

}
