using UnityEngine;
using System.Collections;

public static class WallTexture {

    public static int _MARGIN = 4;

    public static Texture2D Create(int width, int height, Color baseColor, Color pipeColor)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, true);
        tex.name = "Wall Texture";
        tex.mipMapBias = 0;
        tex.filterMode = FilterMode.Trilinear;

        Color[] colors = new Color[width * height];
        for(int i = 0; i < colors.Length; i++)
        {
            colors[i] = baseColor;
        }

        tex.SetPixels(0, 0, width, height, colors);

        tex.Apply();

        return tex;
    }

    private static void ColorFace(Texture2D tex, int xStart, int yStart, int width, int height, Color color)
    {
        Color[] colors = new Color[width * height];
        for(int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }

        tex.SetPixels(xStart, yStart, width, height, colors);
    }
}
