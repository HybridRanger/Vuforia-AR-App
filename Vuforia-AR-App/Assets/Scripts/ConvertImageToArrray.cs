using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class ConvertImageToArrray : MonoBehaviour {

    private string heightMap, overlay;
    public Color[,] colorArray, overlayArray;
    private GameObject terrainMesh;
    private ConvertArrayToMesh atm;
    private Texture2D tex;

	void Start () {
        
        terrainMesh = GameObject.Find("Terrain_Mesh");
        atm = (ConvertArrayToMesh)terrainMesh.GetComponent(typeof(ConvertArrayToMesh));
    }

    public void SetHeightMap (string _heightMap)
    {
        heightMap = _heightMap;
        GenerateArray();
    }

    public void SetOverlay (string _overlay)
    {
        overlay = _overlay;
        GenerateArray();
    }

    public void GenerateArray()
    {
        colorArray = PNGtoArray("Heightmaps", heightMap);
        overlayArray = PNGtoArray("Overlays", overlay);
        atm.UpdateMesh(colorArray, overlayArray);
    }

    Color[,] PNGtoArray(string folder, string file)
    {

        //Texture2D tex = null;
        byte[] fileData;

        BetterStreamingAssets.Initialize();


        fileData = BetterStreamingAssets.ReadAllBytes(folder + "/" + file + ".png");
        tex = new Texture2D(2, 2);
        tex.LoadImage(fileData);


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
