using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class BlockGenerator : MonoBehaviour {

    public bool gizmos;

    public int width;
    public int length;
    public float minHeight;
    public float maxHeight;

    public float alleyWidth;
    public float scale;

    public int texWidth;
    public int texHeight;

    public Color baseColor;
    public Color lightColor;

    [Range(1, 32)]
    public int lightSpacing;
    [Range(0f, 1f)]
    public float lightFrequency;

    void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh = StructureBase.Create(width, length, minHeight, maxHeight, alleyWidth, scale);
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
        mat.mainTexture = WallTexture.Create(texWidth, texHeight, baseColor, Color.white);
        mat.SetTexture("_EmissionMap", WindowTexture.Create(texWidth, texHeight, lightSpacing, lightFrequency, lightColor));
    }
}
