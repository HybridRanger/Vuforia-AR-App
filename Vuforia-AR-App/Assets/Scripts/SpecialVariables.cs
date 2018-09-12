using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialVariables : MonoBehaviour {

    public string path;

    void Start()
    {
        path = Application.streamingAssetsPath;
    }

    //public string StreamingAssetsPath = path;
}
