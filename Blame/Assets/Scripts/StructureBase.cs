using UnityEngine;
using System.Collections;

public static class StructureBase {

    public static Mesh Create(int width, int length, float height, float scale)
    {
        Mesh structureBase = new Mesh();

        structureBase.name = "Unextruded Base " + width + " by " + length;

        structureBase.vertices = FillVertices(width, length, height, scale);

        structureBase.triangles = FillTriangles(width, length);

        structureBase.RecalculateNormals();

        return structureBase;
    }

    private static Vector3[] FillVertices(int width, int length, float height, float scale)
    {
        Vector3[] vertices = new Vector3[width * length * 24];

        for(int i = 0, z = 0; z < length; z++)
        {
            for(int x = 0; x < width; x++, i += 24)
            {
                float rise = Random.Range(0f, height);
                float xPos = ((float)x * scale) - (scale * ((float)width / 2f));
                float zPos = ((float)z * scale) - (scale * ((float)length / 2f));

                //Top Face
                vertices[i] = new Vector3(xPos, rise, zPos);
                vertices[i + 1] = new Vector3(xPos + scale, rise, zPos);
                vertices[i + 2] = new Vector3(xPos, rise, zPos + scale);
                vertices[i + 3] = new Vector3(xPos + scale, rise, zPos + scale);

                //Front Face
                vertices[i + 4] = new Vector3(xPos + scale, 0f, zPos + scale);
                vertices[i + 5] = new Vector3(xPos, 0f, zPos + scale);
                vertices[i + 6] = new Vector3(xPos + scale, rise, zPos + scale);
                vertices[i + 7] = new Vector3(xPos, rise, zPos + scale);

                //Right Face
                vertices[i + 8] = new Vector3(xPos + scale, 0f, zPos);
                vertices[i + 9] = new Vector3(xPos + scale, 0f, zPos + scale);
                vertices[i + 10] = new Vector3(xPos + scale, rise, zPos);
                vertices[i + 11] = new Vector3(xPos + scale, rise, zPos + scale);

                //Back Face
                vertices[i + 12] = new Vector3(xPos, 0f, zPos);
                vertices[i + 13] = new Vector3(xPos + scale, 0f, zPos);
                vertices[i + 14] = new Vector3(xPos, rise, zPos);
                vertices[i + 15] = new Vector3(xPos + scale, rise, zPos);

                //Left Face
                vertices[i + 16] = new Vector3(xPos, 0f, zPos + scale);
                vertices[i + 17] = new Vector3(xPos, 0f, zPos);
                vertices[i + 18] = new Vector3(xPos, rise, zPos + scale);
                vertices[i + 19] = new Vector3(xPos, rise, zPos);

                //Bottom Face
                vertices[i + 20] = new Vector3(xPos, 0f, zPos);
                vertices[i + 21] = new Vector3(xPos + scale, 0f, zPos);
                vertices[i + 22] = new Vector3(xPos, 0f, zPos + scale);
                vertices[i + 23] = new Vector3(xPos + scale, 0f, zPos + scale);
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
