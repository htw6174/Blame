using UnityEngine;
using System.Collections;

public static class ExtrudeBase {

    public static Mesh Extrude(Mesh structureBase, int width, int length, float height)
    {
        Vector3[] verts = structureBase.vertices;

        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                RaiseColumn(verts, width, length, x, z, height);
            }
        }
        structureBase.vertices = verts;

        return structureBase;
    }

    private static void RaiseColumn(Vector3[] verts, int width, int length, int xPos, int zPos, float height)
    {
        float rise = Random.Range(0f, height);

        int startingIndex = (xPos + (zPos * width)) * 4;

        verts[startingIndex] += new Vector3(0f, rise, 0f);
        verts[startingIndex + 1] += new Vector3(0f, rise, 0f);
        verts[startingIndex + 2] += new Vector3(0f, rise, 0f);
        verts[startingIndex + 3] += new Vector3(0f, rise, 0f);
    }
}
