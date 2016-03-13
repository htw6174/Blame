using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WallGenTester : MonoBehaviour {

    public Material wallMaterial;

    public int texWidth, texHeight;

    public Color baseColor, lightColor;

    public int lightSpacing;
    public float lightFrequency;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        wallMaterial.EnableKeyword("_Emission");
        wallMaterial.EnableKeyword("_NormalMap");
        wallMaterial.mainTexture = WallGenerator.Create(texWidth, texHeight, 0, 0f, baseColor, Color.white);
        wallMaterial.SetTexture("_EmissionMap", WindowGenerator.Create(texWidth, texHeight, lightSpacing, lightFrequency, lightColor));
        //wallMaterial.SetTexture("_BumpMap", GreebleGenerator.Create(texWidth, texHeight));
    }
}
