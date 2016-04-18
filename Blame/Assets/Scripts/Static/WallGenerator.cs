using UnityEngine;
using System.Collections;

public static class WallGenerator {

    public static int _MARGIN = 4;

    public static Texture2D Create(int width, int height, int pipeLength, float pipeFrequency, Color baseColor, Color pipeColor)
    {
        int totalWidth = (width * 3) + (_MARGIN * 4);
        int totalHeight = (height * 3) + (_MARGIN * 4);
        Texture2D tex = new Texture2D(totalWidth, totalHeight, TextureFormat.RGB24, true);
        tex.name = "Wall Texture";
        tex.mipMapBias = 0;
        tex.filterMode = FilterMode.Trilinear;

        //Set base color
        ColorFace(tex, 0, 0, totalWidth, totalHeight, baseColor);

        //Color top face
        int xStart = width + (_MARGIN * 2);
        int yStart = height + (_MARGIN * 2);
        ColorFace(tex, xStart, yStart, width, height, Color.blue);

        //Color bottom face
        xStart = _MARGIN;
        yStart = _MARGIN;
        ColorFace(tex, xStart, yStart, width, height, Color.white);

        //Color north face
        xStart = (width) + (_MARGIN * 2);
        yStart = (height * 2) + (_MARGIN * 3);
        ColorFace(tex, xStart, yStart, width, height, Color.red);

        //Color west face
        xStart = _MARGIN;
        yStart = (height) + (_MARGIN * 2);
        ColorFace(tex, xStart, yStart, width, height, Color.cyan);

        //Color east face
        xStart = (width * 2) + (_MARGIN * 3);
        yStart = (height) + (_MARGIN * 2);
        ColorFace(tex, xStart, yStart, width, height, Color.green);

        //Color south face
        xStart = (width) + (_MARGIN * 2);
        yStart = _MARGIN;
        ColorFace(tex, xStart, yStart, width, height, Color.yellow);

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
