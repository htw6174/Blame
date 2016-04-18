﻿using UnityEngine;
using System.Collections;

public static class WindowGenerator {

    public static int _MARGIN = 4;

    /// <summary>
    /// Fills an emissive texture with windows for the vertical sides of a cube texture
    /// X = window   O = no window
    /// O X O
    /// X O X
    /// O X O
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="lightSpacing"></param>
    /// <param name="lightFrequency"></param>
    /// <param name="lightColor"></param>
    /// <returns></returns>
    public static Texture2D Create(int width, int height, int lightSpacing, float lightFrequency, Color lightColor)
    {
        int totalWidth = (width * 3) + (_MARGIN * 4);
        int totalHeight = (height * 3) + (_MARGIN * 4);
        Texture2D tex = new Texture2D(totalWidth, totalHeight, TextureFormat.RGB24, true);
        tex.name = "Window Emissive Texture";
        tex.filterMode = FilterMode.Trilinear;

        //Fill with Color.clear
        FillClear(tex, totalWidth, totalHeight);

        //Fill North Side
        int xStart = width + (_MARGIN * 2);
        int yStart = _MARGIN;
        FillSide(tex, xStart, yStart, width, height, lightSpacing, lightFrequency, lightColor);

        //Fill West Side
        xStart = _MARGIN;
        yStart = height + (_MARGIN * 2);
        FillSide(tex, xStart, yStart, width, height, lightSpacing, lightFrequency, lightColor);

        //Fill East Side
        xStart = (width * 2) + (_MARGIN * 3);
        yStart = height + (_MARGIN * 2);
        FillSide(tex, xStart, yStart, width, height, lightSpacing, lightFrequency, lightColor);

        //Fill South Side
        xStart = width + (_MARGIN * 2);
        yStart = (height * 2) + (_MARGIN * 3);
        FillSide(tex, xStart, yStart, width, height, lightSpacing, lightFrequency, lightColor);

        tex.Apply();

        return tex;
    }

    private static void FillClear(Texture2D tex, int totalWidth, int totalHeight)
    {
        Color[] clearColors = new Color[totalWidth * totalHeight];
        for (int i = 0; i < clearColors.Length; i++)
        {
            clearColors[i] = Color.clear;
        }
        tex.SetPixels(0, 0, totalWidth, totalHeight, clearColors);
    }

    private static void FillSide(Texture2D tex, int xStart, int yStart, int width, int height, int lightSpacing, float lightFrequency, Color lightColor)
    {
        for (int x = 0; x < width / 4; x++)
        {
            for (int y = 0; y < height / 4; y++)
            {
                if (y % lightSpacing == lightSpacing - 1 && lightFrequency > Random.Range(0f, 1f))
                {
                    Color[] lightColors = new Color[16];
                    for (int i = 0; i < lightColors.Length; i++)
                    {
                        lightColors[i] = lightColor;
                    }

                    tex.SetPixels(xStart + (x * 4), yStart + (y * 4), 4, 4, lightColors);
                }
                //else
                //{
                //    Color[] clearColors = new Color[16];
                //    for (int i = 0; i < clearColors.Length; i++)
                //    {
                //        clearColors[i] = Color.clear;
                //    }

                //    tex.SetPixels(xStart + (x * 4), yStart + (y * 4), 4, 4, clearColors);
                //}
            }
        }
    }
}
