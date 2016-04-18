using UnityEngine;
using System.Collections;

public static class GreebleGenerator {

    public static int _MARGIN = 4;

    public static Texture2D Create(int width, int height)
    {
        int totalWidth = (width * 3) + (_MARGIN * 4);
        int totalHeight = (height * 3) + (_MARGIN * 4);
        Texture2D tex = new Texture2D(totalWidth, totalHeight, TextureFormat.ARGB32, true);
        tex.name = "Building Normal Map";
        tex.filterMode = FilterMode.Trilinear;

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
