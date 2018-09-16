using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConvertImageToArrray : MonoBehaviour {

    // Use this for initialization

    public Color[,] colorArray;

	void Start () {
        colorArray = PNGtoArray("/Heightmaps");

        foreach (Color RGB in colorArray)
        {
            Debug.Log(RGB);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Color[,] PNGtoArray(string folder)
    {

        Texture2D tex = null;
        byte[] fileData;
        string filePath = Application.streamingAssetsPath + folder + "/Crater.png";

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            Debug.Log("worked");
        } else
        {
            Debug.Log("broken");
        }

        Color[,] Array = new Color[tex.width, tex.height];

        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                Debug.Log(tex.GetPixel(i, j) * 255);
            }
        }

        return Array;
    }
}
