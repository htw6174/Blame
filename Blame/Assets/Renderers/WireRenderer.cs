using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WireRenderer : MonoBehaviour {

    public Vector3 startPoint;
    public Vector3 endPoint;

    [Range(1, 100)]
    public int segmentCount = 1;

    public float slack;

    [Range(0f, 0.5f)]
    public float thickness;

    public Material material;

    private Mesh _mesh;

    public Mesh sharedMesh
    {
        get
        {
            return _mesh;
        }
    }

    void OnEnable()
    {
        CreateWireMesh();
    }

    void Update()
    {
        Graphics.DrawMesh(sharedMesh, transform.localToWorldMatrix, material, 0, null, 0);
        Graphics.DrawProcedural(MeshTopology.Quads, sharedMesh.vertexCount);
    }
    
    public void CreateWireMesh()
    {
        if (_mesh == null)
        {
            _mesh = new Mesh();
        }

        _mesh.Clear();

        Vector3[] vertices = new Vector3[(segmentCount + 1) * 2];
        Vector3[] normals = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i += 2)
        {
            //Change to a quadratic spline type-thingy
            Vector3 slackPoint = (startPoint + endPoint) / 2f;
            slackPoint[1] -= slack;
            float t = (float)i / (vertices.Length - 1f);
            Vector3 position = Vector3.Lerp(Vector3.Lerp(startPoint, slackPoint, t), Vector3.Lerp(slackPoint, endPoint, t), t);
            vertices[i] = position;
            vertices[i + 1] = position + (Vector3.up * thickness);
            normals[i] = Vector3.back;
            normals[i + 1] = Vector3.back;
        }

        int[] triangles = new int[segmentCount * 4];

        for (int i = 0, v = 0; i < triangles.Length; i += 4, v += 2)
        {
            triangles[i] = v;
            triangles[i + 1] = v + 1;
            triangles[i + 2] = v + 3;
            triangles[i + 3] = v + 2;
        }

        _mesh.vertices = vertices;
        _mesh.normals = normals;
        _mesh.SetIndices(triangles, MeshTopology.Quads, 0);

        ;
        _mesh.UploadMeshData(true);
    }
}
