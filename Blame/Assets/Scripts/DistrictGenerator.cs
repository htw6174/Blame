using UnityEngine;
using System.Collections;

public class DistrictGenerator : Megastructure {

    public int xStructures;
    public int zStructures;

    public float roadWidth;

    public Megastructure structurePrefab;

    public Megastructure[] blocks;

    void Start()
    {
        Debug.Log(structurePrefab.Width);
        Debug.Log(structurePrefab.Length);
        Generate();
    }

    public void Generate()
    {
        UpdateDimensions();

        float xSpacing = structurePrefab.Width + roadWidth;
        float zSpacing = structurePrefab.Length + roadWidth;

        float totalRoadWidth = roadWidth * (xStructures - 1);
        float totalRoadLength = roadWidth * (zStructures - 1);

        float totalWidth = (structurePrefab.Width * (xStructures - 1)) + totalRoadWidth;
        float totalLength = (structurePrefab.Length * (zStructures - 1)) + totalRoadLength;

        blocks = new Megastructure[xStructures * zStructures];

        for (int i = 0, z = 0; z < zStructures; z++)
        {
            for (int x = 0; x < xStructures; x++, i++)
            {
                Megastructure newStructure = Instantiate(structurePrefab);

                float xPos = ((float)x * xSpacing) - ((totalWidth) / 2f);
                float zPos = ((float)z * zSpacing) - (totalLength / 2f);
                Vector3 blockPosition = new Vector3(xPos, 0f, zPos);

                newStructure.transform.SetParent(transform, false);
                newStructure.transform.position = transform.TransformPoint(blockPosition);
            }
        }
    }

    public void UpdateDimensions()
    {
        Width = (structurePrefab.Width * xStructures) + (roadWidth * (xStructures - 1));
        Length = (structurePrefab.Length * zStructures) + (roadWidth * (zStructures - 1));
    }
}
