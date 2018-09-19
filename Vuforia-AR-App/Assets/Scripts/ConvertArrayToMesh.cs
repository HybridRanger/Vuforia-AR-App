using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertArrayToMesh : MonoBehaviour {
    private int xLength, yLength;

    private Vector3[] vertices;
    private int[] triangles;

    public float sclH;

    public void GenerateMesh(Color[,] heightArray, Color[,] overlayArray)
    {
        xLength = heightArray.GetLength(0);
        yLength = heightArray.GetLength(1);

        vertices = new Vector3[xLength * yLength];
        for (int i = 0, y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++, i++)
            {
                vertices[i] = new Vector3(x-(xLength/2), heightArray[x, y].r * sclH * 255, y - (yLength / 2));
                Debug.Log(vertices[i]);
            }
        }

        int[] _triangles = new int[(xLength - 1) * (yLength - 1) * 6];
        int count = 0;

        for (int j = 0; j < yLength - 1; j++)
        {
            for (int i = 0; i < xLength - 1; i++)
            {
                _triangles[count] = ((j) * yLength) + (i);
                _triangles[count + 1] = ((j + 1) * yLength) + (i);
                _triangles[count + 2] = ((j) * yLength) + (i + 1);

                _triangles[count + 3] = ((j + 1) * yLength) + (i);
                _triangles[count + 4] = ((j + 1) * yLength) + (i + 1);
                _triangles[count + 5] = ((j) * yLength) + (i + 1);
                count += 6;
            }
        }
        triangles = _triangles;

        //FlatShading();

        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.colors = GetRandomColors(vertices.Length, heightArray, overlayArray);

        mesh.RecalculateNormals();

        mf.sharedMesh = mesh;
    }

    private Color[] GetRandomColors(int vertexCount, Color[,] heightArray, Color[,] overlayArray)
    {

        var colors = new Color[vertexCount];
        var colorIndex = 0;

        for (int i = 0, y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++, i++)
            {
                if (i % 3 == 0)
                {
                    colorIndex = (int)(heightArray[x, y].r * 255);
                    //Debug.Log(colorIndex);
                }

                colors[i] = overlayArray[0, colorIndex];
                Debug.Log(x + ", " + y + " / " + colorIndex + " / " + colors[i] * 255);
            }
        }

        return colors;
    }


    private void FlatShading()
    {
        Vector3[] flatShadedVertices = new Vector3[triangles.Length];

        for (int i = 0; i < triangles.Length; i++)
        {
            flatShadedVertices[i] = vertices[triangles[i]];
            //Debug.Log(flatShadedVertices[i]);
            triangles[i] = i;
        }

        vertices = flatShadedVertices;
    }
}
