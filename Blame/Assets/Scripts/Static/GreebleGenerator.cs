using UnityEngine;
using System.Collections;

public static class GreebleGenerator {

    public static Texture2D Create(int width, int height)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, true);
        tex.name = "Building Normal Map";
        tex.filterMode = FilterMode.Point;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = new Color(Random.value, Random.value, Random.value);
                tex.SetPixel(x, y, color);
            }
        }
        tex.Apply();

        return tex;
    }
}
