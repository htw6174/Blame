
using UnityEngine;
using System.Collections;
[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class PointCloudGen : MonoBehaviour {

	// Use this for initialization
	// make sure this is a multiple of 3;
	int _pointCount = 24;
	float _fieldRadius = 10;
	void Start () {
		GenerateMesh ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateMesh()
	{
		
		Mesh mesh = new Mesh ();
		Vector3[] verts = new Vector3[_pointCount];
		int[] triangles = new int[_pointCount * 3] ;
		Vector3[] normals = new Vector3[_pointCount];

		for (int pointIDX = 0; pointIDX < _pointCount; pointIDX++)
		{
			
			verts[pointIDX] = Random.onUnitSphere;
			verts[pointIDX].Scale(Vector3.one*_fieldRadius*Random.Range(0f,1f));
			normals[pointIDX] = Vector3.one;
		}
		mesh.vertices = verts;
		for (int triIDX = 0; triIDX < _pointCount; triIDX++) 
		{
			triangles[triIDX * 3] = triIDX;
			triangles[(triIDX * 3) + 1] = triIDX;
			triangles[(triIDX * 3) + 2] = triIDX;

		}
		mesh.triangles = triangles;
		mesh.normals = normals;
		GetComponent<MeshFilter> ().mesh = mesh;

	}
	
}
