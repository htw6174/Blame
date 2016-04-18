using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistrictGenerator : Megastructure {

    public int xStructures;
    public int zStructures;

    public float roadWidth;

    public StructureBlock defaultStructure;

    public StructureBlock[] alternativeStructures;

    private Megastructure[,] structures;

    private int baseStructureSize;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        UpdateDimensions();

        structures = new Megastructure[xStructures, zStructures];

        for (int i = 0; i < alternativeStructures.Length; i++)
        {
            //Debug.Log("Placing structure #" + i);
            FillStructureType(alternativeStructures[i]);
        }

        FillStructureType(defaultStructure);
    }
    
    /// <summary>
    /// Fill grid with some number of a megastructure prefab
    /// </summary>
    /// <param name="structure">Class defining prefab and how much of the total space should be taken up by it</param>
    private void FillStructureType(StructureBlock structure)
    {
        float xSpacing = defaultStructure.prefab.Width;
        float zSpacing = defaultStructure.prefab.Length;

        baseStructureSize = (int)Mathf.Max(xSpacing, zSpacing);

        int structureWidth = Mathf.CeilToInt((float)(structure.prefab.Width - baseStructureSize) / (roadWidth + baseStructureSize)) + 1;
        int structureLength = Mathf.CeilToInt((float)(structure.prefab.Length - baseStructureSize) / (roadWidth + baseStructureSize)) + 1;
        //Debug.Log("Structure is " + structureWidth + " by " + structureLength + " megastructure units");
        int structureArea = structureWidth * structureLength;
        float spawnChance = structure.spawnChance / structureArea;

        float totalRoadWidth = roadWidth * (xStructures - 1);
        float totalRoadLength = roadWidth * (zStructures - 1);

        float totalWidth = (baseStructureSize * (xStructures - 1)) + totalRoadWidth;
        float totalLength = (baseStructureSize * (zStructures - 1)) + totalRoadLength;

        for (int z = 0; z < zStructures; z++)
        {
            for(int x = 0; x < xStructures; x++)
            {
                if (Random.value < spawnChance)
                {
                    PlaceStructure(x, z, structureWidth, structureLength, totalWidth, totalLength, structure.prefab);
                }
            }
        }
    }

    private void PlaceStructure(int x, int z, int width, int length, float totalWidth, float totalLength, Megastructure structure)
    {
        bool flipped = Random.value > 0.5f ? true : false;

        if (flipped)
        {
            int temp = width;
            width = length;
            length = temp;
        }

        if (z + length > zStructures || x + width > xStructures)
        {
            return;
        }

        //Check if other blocks in range are occupied
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (structures[x + j, z + i] != null)
                {
                    return;
                }
            }
        }

        //Debug.Log("Placing structure at " + x + ", " + z);
        Megastructure newStructure = Instantiate(structure) as Megastructure;

        float xMinPos = ((float)x * (defaultStructure.prefab.Width + roadWidth)) - ((totalWidth) / 2f);
        float zMinPos = ((float)z * (defaultStructure.prefab.Length + roadWidth)) - (totalLength / 2f);
        Vector3 blockMinPosition = new Vector3(xMinPos, 0f, zMinPos);

        float xMaxPos = ((float)(x + width - 1) * (defaultStructure.prefab.Width + roadWidth)) - ((totalWidth) / 2f);
        float zMaxPos = ((float)(z + length - 1) * (defaultStructure.prefab.Length + roadWidth)) - (totalLength / 2f);
        Vector3 blockMaxPosition = new Vector3(xMaxPos, 0f, zMaxPos);

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                structures[x + j, z + i] = newStructure;
            }
        }

        newStructure.transform.SetParent(transform, false);
        newStructure.transform.position = transform.TransformPoint(Vector3.Lerp(blockMinPosition, blockMaxPosition, 0.5f));
        if (flipped) newStructure.transform.Rotate(Vector3.up * 90f);
    }

    public void UpdateDimensions()
    {
        Width = Mathf.CeilToInt((defaultStructure.prefab.Width * xStructures) + (roadWidth * (xStructures - 1)));
        Length = Mathf.CeilToInt((defaultStructure.prefab.Length * zStructures) + (roadWidth * (zStructures - 1)));
    }
}
