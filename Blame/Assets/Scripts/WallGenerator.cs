using UnityEngine;
using System.Collections;

public static class WallGenerator {

    public static Texture2D Create(int width, int height, int pipeLength, float pipeFrequency, Color baseColor, Color pipeColor)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, true);
        tex.name = "Wall Texture";
        tex.filterMode = FilterMode.Point;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tex.SetPixel(x, y, baseColor);
            }
        }
        tex.Apply();

        return tex;
    }
}
