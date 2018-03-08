using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
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
        Width = Mathf.CeilToInt(maxDimensions.x);
        Length = Mathf.CeilToInt(maxDimensions.z);
    }

    public void Generate()
    {
        UpdateDimensions();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh = BuildingMesh.Create(minDimensions, maxDimensions, minBlocks, maxBlocks, overlap, tilesPerWorldUnit);
        AssignMesh(mesh);
        AssignCollider(mesh);
        //AssignMaterial();
    }

    private void AssignMesh(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void AssignCollider(Mesh mesh)
    {
        MeshCollider collider = GetComponent<MeshCollider>();
        collider.sharedMesh = mesh;
    }

    private void AssignMaterial()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_Emission");
        mat.EnableKeyword("_NormalMap");
        mat.mainTexture = WallTexture.Create(texWidth, texHeight, baseColor, Color.white);
        mat.SetTexture("_EmissionMap", WindowTexture.Create(texWidth, texHeight, lightSpacing, lightFrequency, lightColor));
        mat.SetTexture("_BumpMap", GreebleGenerator.Create(texWidth, texHeight));
    }
}
