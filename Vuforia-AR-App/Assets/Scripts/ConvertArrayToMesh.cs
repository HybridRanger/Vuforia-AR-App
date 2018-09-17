using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConvertArrayToMesh : MonoBehaviour {
    public float scl, hScl;
    public int xLength, yLength;
    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ArrayToMesh (Color[,] colorValues, Color[,] overlayArray)
    {
        Debug.Log("ArrayToMesh Run Success");

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        xLength = colorValues.GetLength(0);
        yLength = colorValues.GetLength(1);     //get the x and y lengths of the array
        int[] heightValues = new int[xLength*yLength];

        int count = 0;

        for (int j = 0; j < yLength; j++)
        {
            for (int i = 0; i < xLength; i++)
            {
                heightValues[count] = ((int)colorValues[i, j].r + (int)colorValues[i,j].g + (int)colorValues[i,j].b)/3;     //set the height value to the RGB average
                //Debug.Log(heightValues[count]);
                count++;
            }
        }

        mesh.vertices = Vertices(heightValues);
        mesh.triangles = Triangles();
        mesh.RecalculateNormals();
        mesh.colors = Colors(colorValues, overlayArray);
    }

    Vector3[] Vertices (int[] HeightValues)
    {
        Vector3[] vertices = new Vector3[xLength*yLength];
        int count = 0;
        for (int j = 0; j < yLength; j++) {
            for (int i = 0; i < xLength; i++)
            {
                vertices[count] = new Vector3(i*scl, HeightValues[count]*hScl, j*scl);
                count++;
            }
        }

        return vertices;
    }

    int[] Triangles()
    {
        List<int> triList = new List<int>();
        for (int j = 0; j < yLength-1; j++)
        {
            for (int i = 0; i < xLength-1; i++)
            {
                triList.Add(((j) * yLength) + (i));
                triList.Add(((j + 1) * yLength) + (i));
                triList.Add(((j) * yLength) + (i + 1));

                triList.Add(((j + 1) * yLength) + (i));
                triList.Add(((j + 1) * yLength) + (i + 1));
                triList.Add(((j) * yLength) + (i + 1));
            }
        }

        int[] array = triList.ToArray();
        return array;
    }

    Color[] Colors(Color[,] colorValues, Color[,] overlayArray)
    {
        Color[] colors = new Color[xLength * yLength];
        int count = 0;
        for (int j = 0; j < yLength; j++)
        {
            for (int i = 0; i < xLength; i++)
            {
                int average = ((int)colorValues[i, j].r + (int)colorValues[i, j].g + (int)colorValues[i, j].b) / 3;
                colors[count] = overlayArray[0, average-1];
                Debug.Log(colors[count]);
                count++;
            }
        }
        return colors;
    }

    Vector2[] UVs()
    {

        return null;
    }

    Vector2[] Normals()
    {
        Vector2[] normals = new Vector2[xLength * yLength];

        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = new Vector2();
        }

        return null;
    }
}
