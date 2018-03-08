using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenTester : MonoBehaviour {

    public Vector2 regionSize;
    public int regions;

    void OnEnable()
    {
        Generate();
    }

    public void Generate()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh;
        if (meshFilter.sharedMesh == null)
        {
            mesh = new Mesh();
        }
        else
        {
            mesh = meshFilter.sharedMesh;
        }
        VoroniMeshGenerator.GenerateVoroniMesh(mesh, regionSize.x, regionSize.y, regions);
        meshFilter.sharedMesh = mesh;
    }
}
