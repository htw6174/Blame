using UnityEngine;
using System.Collections;

public static class BuildingGenerator {
    
    public static Mesh Create(Vector3 minBlockSize, Vector3 maxBlockSize, int minBlocks, int maxBlocks, float overlap, float tilesPerWorldUnit)
    {
        Mesh building = new Mesh();

        Vector3 dimensions = new Vector3(Random.Range(minBlockSize.x, maxBlockSize.x), Random.Range(minBlockSize.x, maxBlockSize.x), Random.Range(minBlockSize.x, maxBlockSize.x));
        int blocks = Random.Range(minBlocks, maxBlocks);

        Vector3[] vertices = FillVertices(minBlockSize, maxBlockSize, blocks, overlap);

        building.name = "Building";

        building.vertices = vertices;

        building.triangles = FillTriangles(blocks);

        building.uv = FillUV(blocks, tilesPerWorldUnit, vertices);

        building.RecalculateNormals();

        building.RecalculateBounds();

        return building;
    }

    //Depreciated, could have some other use?
    private static float[] FillHeights(float[] heights, float minHeight, float maxHeight)
    {
        for (int i = 0; i < heights.Length; i++)
        {
            heights[i] = Random.Range(minHeight, maxHeight);
        }

        return heights;
    }

    private static Vector3[] FillVertices(Vector3 minBlockSize, Vector3 maxBlockSize, int blocks, float overlap)
    {
        Vector3[] vertices = new Vector3[blocks * 24];

        float blockBase = 0f;
        float lastBlockHeight = 0f;

        for (int v = 0, b = 0; b < blocks; b++, v += 24)
        {

            float xLow = -Random.Range(minBlockSize.x, maxBlockSize.x) / 2f;
            float yLow = blockBase - (overlap * lastBlockHeight);
            float zLow = -Random.Range(minBlockSize.z, maxBlockSize.z) / 2f;

            float xHigh = Random.Range(minBlockSize.x, maxBlockSize.x) / 2f;
            float yHigh = Random.Range(minBlockSize.y, maxBlockSize.y) + yLow;
            float zHigh = Random.Range(minBlockSize.z, maxBlockSize.z) / 2f;

            blockBase = yHigh;
            lastBlockHeight = yHigh - yLow;

            //Top Face
            vertices[v] = new Vector3(xLow, yHigh, zLow);
            vertices[v + 1] = new Vector3(xHigh, yHigh, zLow);
            vertices[v + 2] = new Vector3(xLow, yHigh, zHigh);
            vertices[v + 3] = new Vector3(xHigh, yHigh, zHigh);

            //Front Face
            vertices[v + 4] = new Vector3(xHigh, yLow, zHigh);
            vertices[v + 5] = new Vector3(xLow, yLow, zHigh);
            vertices[v + 6] = new Vector3(xHigh, yHigh, zHigh);
            vertices[v + 7] = new Vector3(xLow, yHigh, zHigh);

            //Right Face
            vertices[v + 8] = new Vector3(xHigh, yLow, zLow);
            vertices[v + 9] = new Vector3(xHigh, yLow, zHigh);
            vertices[v + 10] = new Vector3(xHigh, yHigh, zLow);
            vertices[v + 11] = new Vector3(xHigh, yHigh, zHigh);

            //Back Face
            vertices[v + 12] = new Vector3(xLow, yLow, zLow);
            vertices[v + 13] = new Vector3(xHigh, yLow, zLow);
            vertices[v + 14] = new Vector3(xLow, yHigh, zLow);
            vertices[v + 15] = new Vector3(xHigh, yHigh, zLow);

            //Left Face
            vertices[v + 16] = new Vector3(xLow, yLow, zHigh);
            vertices[v + 17] = new Vector3(xLow, yLow, zLow);
            vertices[v + 18] = new Vector3(xLow, yHigh, zHigh);
            vertices[v + 19] = new Vector3(xLow, yHigh, zLow);

            //Bottom Face
            vertices[v + 20] = new Vector3(xLow, yLow, zHigh);
            vertices[v + 21] = new Vector3(xHigh, yLow, zHigh);
            vertices[v + 22] = new Vector3(xLow, yLow, zLow);
            vertices[v + 23] = new Vector3(xHigh, yLow, zLow);
        }

        return vertices;
    }

    private static int[] FillTriangles(int blocks)
    {
        int[] tris = new int[blocks * 12 * 3];

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

    private static Vector2[] FillUV(int blocks, float tilesPerWorldUnit, Vector3[] vertices)
    {
        Vector2[] uv = new Vector2[blocks * 24];

        //Loop through blocks one at a time
        for (int v = 0, b = 0; b < blocks; b++)
        {
            float blockHeight = vertices[v].y - vertices[v + 23].y;

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
                float sideWidth = Vector3.Distance(vertices[v], vertices[v + 1]);

                uv[v] = Vector2.zero;
                uv[v + 1] = Vector2.right * sideWidth * tilesPerWorldUnit;
                uv[v + 2] = Vector2.up * blockHeight * tilesPerWorldUnit;
                uv[v + 3] = uv[v + 1] + uv[v + 2];
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

        for (int n = 0; n < normals.Length; n++)
        {
            normals[n] = Vector3.up;
        }

        return normals;
    }
}
