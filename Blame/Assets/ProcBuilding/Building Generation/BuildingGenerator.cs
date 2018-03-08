using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class BuildingGenerator : MonoBehaviour {

    public Vector3 minDimensions;
    public Vector3 maxDimensions;

    public int minBlocks, maxBlocks;

    [Range(0f, 1f)]
    public float overlap;

    public float tilesPerWorldUnit;

	void Start () {
		Generate();
	}

    public void Generate()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh = BuildingMesh.Create(minDimensions, maxDimensions, minBlocks, maxBlocks, overlap, tilesPerWorldUnit);
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
