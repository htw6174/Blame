using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BuildingGenTester : Megastructure {

    public Vector3 minDimensions;
    public Vector3 maxDimensions;

    public int minBlocks, maxBlocks;

    [Range(0f, 1f)]
    public float overlap;

    public float tilesPerWorldUnit;

    public int texWidth, texHeight;

    public Color baseColor, lightColor;

    public int lightSpacing;
    public float lightFrequency;

    void Awake()
    {
        Generate();
    }

    public void UpdateDimensions()
    {
        Width = maxDimensions.x;
        Length = maxDimensions.z;
        Debug.Log(Width + ", " + Length);
    }

    public void Generate()
    {
        UpdateDimensions();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh = BuildingGenerator.Create(minDimensions, maxDimensions, minBlocks, maxBlocks, overlap, tilesPerWorldUnit);
        AssignMesh(mesh);
        AssignMaterial();
    }

    private void AssignMesh(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void AssignMaterial()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_Emission");
        mat.mainTexture = WallGenerator.Create(texWidth, texHeight, 0, 0f, baseColor, Color.white);
        mat.SetTexture("_EmissionMap", WindowGenerator.Create(texWidth, texHeight, lightSpacing, lightFrequency, lightColor));
    }
}
