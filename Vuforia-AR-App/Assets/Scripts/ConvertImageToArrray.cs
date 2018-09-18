using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConvertImageToArrray : MonoBehaviour {

    // Use this for initialization

    public Color[,] colorArray, overlayArray;
    private GameObject terrainMesh;

	void Start () {
        colorArray = PNGtoArray("/Heightmaps", "/Breca.png");
        overlayArray = PNGtoArray("/Overlays", "/RGB.png");
        terrainMesh = GameObject.Find("Terrain_Mesh");
        ConvertArrayToMesh atm = (ConvertArrayToMesh)terrainMesh.GetComponent(typeof(ConvertArrayToMesh));
        atm.GenerateMesh(colorArray, overlayArray);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Color[,] PNGtoArray(string folder, string file)
    {

        Texture2D tex = null;
        byte[] fileData;
        string filePath = Application.streamingAssetsPath + folder + file;

        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            Debug.Log("Load Image Success");
        } else
        {
            Debug.Log("Load Image Fail");
        }

        Color[,] Array = new Color[tex.width, tex.height];

        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                Array[i,j] = (tex.GetPixel(i, j));
            }
        }

        return Array;
    }
}
