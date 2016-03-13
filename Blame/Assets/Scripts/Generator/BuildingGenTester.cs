using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BuildingGenTester : Megastructure {

    public Vector3 minDimensions;
    public Vector3 maxDimensions;

    public int minBlocks, maxBlocks;

    [Range(0f, 1f)]
    public float overlap;

    public Vector3 focalPoint;

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
        Width = Mathf.CeilToInt(maxDimensions.x);
        Length = Mathf.CeilToInt(maxDimensions.z);
    }

    public void Generate()
    {
        UpdateDimensions();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        float focalRadius = Vector3.Distance(transform.position, focalPoint);
        mesh = BuildingGenerator.Create(minDimensions, maxDimensions, minBlocks, maxBlocks, overlap, tilesPerWorldUnit, focalRadius);
        AssignMesh(mesh);
        //AssignMaterial();
    }

    private void AssignMesh(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void AssignMaterial()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_Emission");
        mat.EnableKeyword("_NormalMap");
        mat.mainTexture = WallGenerator.Create(texWidth, texHeight, 0, 0f, baseColor, Color.white);
        mat.SetTexture("_EmissionMap", WindowGenerator.Create(texWidth, texHeight, lightSpacing, lightFrequency, lightColor));
        mat.SetTexture("_BumpMap", GreebleGenerator.Create(texWidth, texHeight));
    }
}
