using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class CityGenTester : MonoBehaviour {

    public bool gizmos;

    public int width;
    public int length;
    public float maxHeight;

    public float scale;

    void Awake()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh = StructureBase.Create(width, length, maxHeight, scale);
        //mesh = ExtrudeBase.Extrude(mesh, width, length, maxHeight);
        AssignMesh(mesh);
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
