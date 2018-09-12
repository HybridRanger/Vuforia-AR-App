using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFolder : MonoBehaviour {

    public void GetFolderContents(string folderName)
    {
        string path = "Assets/" + folderName;

        foreach (string file in System.IO.Directory.GetFiles(path))
        {
            Debug.Log(file);
        }
    }

}
