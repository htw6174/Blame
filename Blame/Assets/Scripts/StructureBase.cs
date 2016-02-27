using UnityEngine;
using System.Collections;

public static class StructureBase {

    public static Mesh Create(int width, int length, float minHeight, float maxHeight, float alleyWidth, float scale)
    {
        if(width > 50)
        {
            width = 50;
            Debug.Log("Maximum dimensions are 50x50!");
        }
        if(length > 50)
        {
            length = 50;
            Debug.Log("Maximum dimensions are 50x50!");
        }

        Mesh structureBase = new Mesh();

        float[] columnHeights = new float[width * length];
        FillHeights(columnHeights, minHeight, maxHeight);

        structureBase.name = "Unextruded Base " + width + " by " + length;

        structureBase.vertices = FillVertices(width, length, columnHeights, alleyWidth, scale);

        structureBase.triangles = FillTriangles(width, length);

        structureBase.uv = FillUV(width, length, columnHeights, scale);

        structureBase.RecalculateNormals();

        return structureBase;
    }

    private static float[] FillHeights(float[] heights, float minHeight, float maxHeight)
    {
        for(int i = 0; i < heights.Length; i++)
        {
            heights[i] = Random.Range(minHeight, maxHeight);
        }

        return heights;
    }

    private static Vector3[] FillVertices(int width, int length, float[] heights, float alleyWidth, float scale)
    {
        Vector3[] vertices = new Vector3[width * length * 24];

        for(int i = 0, z = 0; z < length; z++)
        {
            for(int x = 0; x < width; x++, i += 24)
            {
                float rise = heights[x + (z * width)];

                float xMin = ((float)x * scale) - (scale * ((float)width / 2f)) + (alleyWidth / 2f);
                float zMin = ((float)z * scale) - (scale * ((float)length / 2f)) + (alleyWidth / 2f);

                float xMax = ((float)x * scale) - (scale * ((float)width / 2f)) + scale - (alleyWidth / 2f);
                float zMax = ((float)z * scale) - (scale * ((float)length / 2f)) + scale - (alleyWidth / 2f);

                //Top Face
                vertices[i] = new Vector3(xMin, rise, zMin);
                vertices[i + 1] = new Vector3(xMax, rise, zMin);
                vertices[i + 2] = new Vector3(xMin, rise, zMax);
                vertices[i + 3] = new Vector3(xMax, rise, zMax);

                //Front Face
                vertices[i + 4] = new Vector3(xMax, 0f, zMax);
                vertices[i + 5] = new Vector3(xMin, 0f, zMax);
                vertices[i + 6] = new Vector3(xMax, rise, zMax);
                vertices[i + 7] = new Vector3(xMin, rise, zMax);

                //Right Face
                vertices[i + 8] = new Vector3(xMax, 0f, zMin);
                vertices[i + 9] = new Vector3(xMax, 0f, zMax);
                vertices[i + 10] = new Vector3(xMax, rise, zMin);
                vertices[i + 11] = new Vector3(xMax, rise, zMax);

                //Back Face
                vertices[i + 12] = new Vector3(xMin, 0f, zMin);
                vertices[i + 13] = new Vector3(xMax, 0f, zMin);
                vertices[i + 14] = new Vector3(xMin, rise, zMin);
                vertices[i + 15] = new Vector3(xMax, rise, zMin);

                //Left Face
                vertices[i + 16] = new Vector3(xMin, 0f, zMax);
                vertices[i + 17] = new Vector3(xMin, 0f, zMin);
                vertices[i + 18] = new Vector3(xMin, rise, zMax);
                vertices[i + 19] = new Vector3(xMin, rise, zMin);

                //Bottom Face
                vertices[i + 20] = new Vector3(xMin, 0f, zMax);
                vertices[i + 21] = new Vector3(xMax, 0f, zMax);
                vertices[i + 22] = new Vector3(xMin, 0f, zMin);
                vertices[i + 23] = new Vector3(xMax, 0f, zMin);
            }
        }

        return vertices;
    }

    private static int[] FillTriangles(int width, int length)
    {
        int[] tris = new int[width * length * 12 * 3];

        for (int v = 0, t = 0; t < tris.Length; v += 24, t += 36)
        {
            for (int i = 0; i < 6; i++)
            {
                tris[t + (i * 6) + 0] = v + (i * 4) + 0;
                tris[t + (i * 6) + 1] = v + (i * 4) + 2;
                tris[t + (i * 6) + 2] = v + (i * 4) + 1;

                tris[t + (i * 6) + 3] = v + (i * 4) + 3;
                tris[t + (i * 6) + 4] = v + (i * 4) + 1;
                tris[t + (i * 6) + 5] = v + (i * 4) + 2;
            }
        }

        return tris;
    }

    private static Vector2[] FillUV(int width, int length, float[] heights, float scale)
    {
        Vector2[] uv = new Vector2[width * length * 24];

        //Loop through blocks one at a time
        for(int v = 0, b = 0; b < width * length; b++)
        {
            float heightRatio = heights[b] / scale;

            //Set top face uvs
            uv[v++] = Vector2.zero;
            uv[v++] = Vector2.zero;
            uv[v++] = Vector2.zero;
            uv[v++] = Vector2.zero;
            //uv[v++] = Vector2.zero;
            //uv[v++] = Vector2.right;
            //uv[v++] = Vector2.up;
            //uv[v++] = Vector2.one;

            //Loop through sides 4 verts at a time
            for (int s = 0; s < 4; s++, v += 4)
            {
                uv[v] = Vector2.zero;
                uv[v + 1] = Vector2.right;
                uv[v + 2] = Vector2.up * heightRatio;
                uv[v + 3] = new Vector2(1f, heightRatio);
            }

            //Set bottom face uvs
            uv[v++] = Vector2.zero;
            uv[v++] = Vector2.zero;
            uv[v++] = Vector2.zero;
            uv[v++] = Vector2.zero;
        }

        return uv;
    }

    private static Vector3[] FillNormals(int width, int length)
    {
        Vector3[] normals = new Vector3[width * length * 4];

        for(int n = 0; n < normals.Length; n++)
        {
            normals[n] = Vector3.up;
        }

        return normals;
    }
}
