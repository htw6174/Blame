using UnityEngine;
using System.Collections;

public static class BuildingGenerator {
    
    public static Mesh Create(Vector3 minBlockSize, Vector3 maxBlockSize, int minBlocks, int maxBlocks, float overlap, float tilesPerWorldUnit, float focalRadius = 0f)
    {
        Mesh building = new Mesh();

        Vector3 dimensions = new Vector3(Random.Range(minBlockSize.x, maxBlockSize.x), Random.Range(minBlockSize.x, maxBlockSize.x), Random.Range(minBlockSize.x, maxBlockSize.x));
        int blocks = Random.Range(minBlocks, maxBlocks);

        Vector3[] vertices = FillVertices(minBlockSize, maxBlockSize, blocks, overlap, focalRadius);

        building.name = "Building";

        building.vertices = vertices;

        building.triangles = FillTriangles(blocks);

        building.uv = FillUV(blocks, tilesPerWorldUnit, vertices);

        building.uv2 = FillUV2(blocks, tilesPerWorldUnit, vertices);

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

    private static Vector3[] FillVertices(Vector3 minBlockSize, Vector3 maxBlockSize, int blocks, float overlap, float focalRadius = 0f)
    {
        Vector3[] vertices = new Vector3[blocks * 24];

        float buildingsOnOuterCircumference = 2f * focalRadius * Mathf.PI / maxBlockSize.x;
        float buildingsOnInnerCircumference = 2f * (focalRadius - maxBlockSize.z) * Mathf.PI / maxBlockSize.x;

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

    private static Vector3[] FillNormals(int blocks)
    {
        Vector3[] normals = new Vector3[blocks * 24];

        return normals;
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

            //Define uv positions
            Vector2 topBottomLeft = new Vector2(1f / 3f, 1f / 3f);
            Vector2 topBottomRight = new Vector2(2f / 3f, 1f / 3f);
            Vector2 topTopLeft = new Vector2(1f / 3f, 2f / 3f);
            Vector2 topTopRight = new Vector2(2f / 3f, 2f / 3f);

            Vector2 bottomBottomLeft = new Vector2(0f, 0f);
            Vector2 bottomBottomRight = new Vector2(1f / 3f, 0f);
            Vector2 bottomTopLeft = new Vector2(0f, 1f / 3f);
            Vector2 bottomTopRight = topBottomLeft;

            Vector2 northBottomLeft = topTopLeft;
            Vector2 northBottomRight = topTopRight;
            Vector2 northTopLeft = new Vector2(1f / 3f, 1f);
            Vector2 northTopRight = new Vector2(2f / 3f, 1f);

            Vector2 westBottomLeft = new Vector2(0f, 1f / 3f);
            Vector2 westBottomRight = topBottomLeft;
            Vector2 westTopLeft = new Vector2(0f, 2f / 3f);
            Vector2 westTopRight = topTopLeft;

            Vector2 eastBottomLeft = topBottomRight;
            Vector2 eastBottomRight = new Vector2(1f, 1f / 3f);
            Vector2 eastTopLeft = topTopRight;
            Vector2 eastTopRight = new Vector2(1f, 2f / 3f);

            Vector2 southBottomLeft = new Vector2(1f / 3f, 0f);
            Vector2 southBottomRight = new Vector2(2f / 3f, 0f);
            Vector2 southTopLeft = topBottomLeft;
            Vector2 southTopRight = topBottomRight;

            //Set top face uvs
            uv[v++] = topBottomLeft;
            uv[v++] = topBottomRight;
            uv[v++] = topTopLeft;
            uv[v++] = topTopRight;
            //uv[v++] = Vector2.zero;
            //uv[v++] = Vector2.right;
            //uv[v++] = Vector2.up;
            //uv[v++] = Vector2.one;

            //Loop through sides 4 verts at a time
            for (int s = 0; s < 4; s++, v += 4)
            {
                float sideWidth = Vector3.Distance(vertices[v], vertices[v + 1]);

                uv[v] = northBottomLeft;
                uv[v + 1] = northBottomRight; //Vector2.right * sideWidth * tilesPerWorldUnit;
                uv[v + 2] = northTopLeft; //Vector2.up * blockHeight * tilesPerWorldUnit;
                uv[v + 3] = northTopRight; //uv[v + 1] + uv[v + 2];
            }

            //Set bottom face uvs
            uv[v++] = bottomBottomLeft;
            uv[v++] = bottomBottomRight;
            uv[v++] = bottomTopLeft;
            uv[v++] = bottomTopRight;
        }

        return uv;
    }

    private static Vector2[] FillUV2(int blocks, float tilesPerWorldUnit, Vector3[] vertices)
    {
        Vector2[] uv2 = new Vector2[blocks * 24];

        for(int v = 0, b = 0; b < blocks; b++)
        {
            float blockHeight = vertices[v].y - vertices[v + 23].y;

            float width = Vector3.Distance(vertices[v], vertices[v + 1]);
            float length = Vector3.Distance(vertices[v], vertices[v + 2]);

            //Set top face
            uv2[v] = Vector2.zero;
            uv2[v + 1] = Vector2.right * width * tilesPerWorldUnit;
            uv2[v + 2] = Vector2.up * length * tilesPerWorldUnit;
            uv2[v + 3] = uv2[v + 1] + uv2[v + 2];

            v += 4;

            //Loop through sides 4 verts at a time
            for (int s = 0; s < 4; s++, v += 4)
            {
                float sideWidth = Vector3.Distance(vertices[v], vertices[v + 1]);

                uv2[v] = Vector2.zero;
                uv2[v + 1] = Vector2.right * sideWidth * tilesPerWorldUnit;
                uv2[v + 2] = Vector2.up * blockHeight * tilesPerWorldUnit;
                uv2[v + 3] = uv2[v + 1] + uv2[v + 2];
            }

            //Set bottom face
            uv2[v] = Vector2.zero;
            uv2[v + 1] = Vector2.right * width * tilesPerWorldUnit;
            uv2[v + 2] = Vector2.up * length * tilesPerWorldUnit;
            uv2[v + 3] = uv2[v + 1] + uv2[v + 2];

            v += 4;
        }

        return uv2;
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
