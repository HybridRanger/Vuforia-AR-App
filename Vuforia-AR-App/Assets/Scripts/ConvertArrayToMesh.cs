using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConvertArrayToMesh : MonoBehaviour {
    public float scl = 1, hScl = 0.1f;
    ///Color[,] colorValues;
	// Use this for initialization
	void Start () {
        
        //colorValues = colors;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ArrayToMesh ()
    {
        Color[,] colorValues = GameObject.Find("_Manager").GetComponent<ConvertImageToArrray>().colorArray;
        int xLength = colorValues.GetLength(0), yLength = colorValues.GetLength(1);     //get the x and y lengths of the array
        int[] heightValues = new int[xLength*yLength];
        int count = 0;
        for (int j = 0; j < yLength; j++)
        {
            for (int i = 0; i < xLength; i++)
            {
                heightValues[count] = ((int)colorValues[i, j].r + (int)colorValues[i,j].g + (int)colorValues[i,j].b)/3;     //set the height value to the RGB average
                count++;
            }
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        mesh.vertices = Vertices(heightValues, xLength, yLength);

    }

    Vector3[] Vertices (int[] HeightValues, int xLength, int yLength)
    {
        Vector3[] vertices = new Vector3[xLength*yLength];
        int count = 0;
        for (int j = 0; j < yLength; j++) {
            for (int i = 0; i < xLength; i++)
            {
                vertices[count] = new Vector3(xLength*scl, HeightValues[count]*hScl, yLength*scl);
                count++;
            }
        }
        

        return vertices;
    }
}
