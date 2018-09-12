using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFolder : MonoBehaviour {

    public void GetFolderContents(string folderName)
    {
        
        string path = GameObject.Find("_Manager").GetComponent<SpecialVariables>().path + "/" + folderName;

        //Debug.Log(path);

        
        foreach (string file in System.IO.Directory.GetFiles(path))
        {
            if (file.Contains(".png") && !file.Contains(".meta"))
            {
                Debug.Log(file);
            }
        }
        
    }

}
