using UnityEngine;
using System.Collections;

public static class WindowGenerator {

    public static Texture2D Create(int width, int height, int lightSpacing, float lightFrequency, Color lightColor)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, true);
        tex.name = "Window Emissive Texture";
        tex.filterMode = FilterMode.Point;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (y % lightSpacing == lightSpacing - 1 && lightFrequency > Random.Range(0f, 1f))
                {
                    tex.SetPixel(x, y, lightColor);
                }
                else
                {
                    tex.SetPixel(x, y, Color.clear);
                }
            }
        }
        tex.Apply();

        return tex;
    }
}
