using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;

public class SelectFile : MonoBehaviour
{
	string filePath;
    private void Start()
    {
        //OpenSelector();


    }

	public void idgaf() {
		StartCoroutine(ShowLoadDialogCoroutine());
	}


    public void OpenSelector() {
        //filePath = EditorUtility.OpenFilePanel("Select Song", "", "wav");
        string newPath = Application.dataPath + "/Resources/Custom Songs/" + System.IO.Path.GetFileName(filePath);
		//FileUtil.CopyFileOrDirectory(filePath, newPath);
		System.IO.File.Copy(filePath, newPath);

		SceneManager.LoadScene(0);
        
    }

	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");
		filePath = FileBrowser.Result;
		OpenSelector();

		// Dialog is closed
		// Print whether a file is chosen (FileBrowser.Success)
		// and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
		Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

		if (FileBrowser.Success)
		{
			// If a file was chosen, read its bytes via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result);
		}
	}


}
