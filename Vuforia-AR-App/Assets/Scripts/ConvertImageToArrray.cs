using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConvertImageToArrray : MonoBehaviour {

    // Use this for initialization

    public string heightMap, overlay;
    public Color[,] colorArray, overlayArray;
    private GameObject terrainMesh;
    private ConvertArrayToMesh atm;

	void Start () {
        terrainMesh = GameObject.Find("Terrain_Mesh");
        atm = (ConvertArrayToMesh)terrainMesh.GetComponent(typeof(ConvertArrayToMesh));
        atm.GenerateMesh(colorArray, overlayArray);
    }
	
	// Update is called once per frame
	void Update () {
        colorArray = PNGtoArray("/Heightmaps", "/" + heightMap + ".png");
        overlayArray = PNGtoArray("/Overlays", "/" + overlay + ".png");
    }

    public void GenerateArray()
    {
        atm.GenerateMesh(colorArray, overlayArray);
    }

    static Color[,] PNGtoArray(string folder, string file)
    {

        Texture2D tex = null;
        byte[] fileData;
        string filePath;

#if UNITY_EDITOR
        //filePath = Application.streamingAssetsPath + folder + file;
#endif

#if UNITY_ANDROID
        filePath = "jar:file://" + Application.dataPath + "!/assets/" + folder + file;
#endif
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
