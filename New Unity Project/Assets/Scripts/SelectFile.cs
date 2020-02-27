using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class SelectFile : MonoBehaviour
{
    AudioSource source;
    private void Start()
    {
        //OpenSelector();
        source = GetComponent<AudioSource>();
    }
    public void OpenSelector() {
        string filePath = EditorUtility.OpenFilePanel("Overwrite with png", "", "wav");
        WWW audioPath = new WWW(filePath);
        AudioClip clip = audioPath.GetAudioClip();
        SerializeClip(clip, filePath);
        //SaveClip(clip);
    }


    void SerializeClip(AudioClip clip, string oldPath) {
        BinaryFormatter formatter = new BinaryFormatter();

        float[] clipData = new float[clip.samples * clip.channels];
        clip.SetData(clipData, 0);

        //string path = "C:/Users/atiny/Desktop/" + System.IO.Path.GetFileName(oldPath);
        string path = Application.dataPath + "/Resources/Custom Songs/" + System.IO.Path.GetFileName(oldPath);

        FileUtil.CopyFileOrDirectory(oldPath, path);



    }
}
