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

        Material mat = GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_Emission");
        mat.mainTexture = WallGenerator.Create(texWidth, texHeight, 0, 0f, baseColor, Color.white);
        mat.SetTexture("_EmissionMap", WindowGenerator.Create(texWidth, texHeight, lightSpacing, lightFrequency, lightColor));
    }

    private void AssignMesh(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void OnDrawGizmos()
    {
        Mesh mesh;
        if((mesh = GetComponent<MeshFilter>().sharedMesh) && gizmos)
        {
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                Gizmos.color = new Color(1f, 1f - ((float)i / mesh.vertexCount), 1f - ((float)i / mesh.vertexCount));
                Gizmos.DrawCube(mesh.vertices[i], Vector3.one * 0.2f);
            }
        }
    }
}
