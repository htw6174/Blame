using UnityEngine;
using System.Collections;

[System.Serializable]
public class StructureBlock {

    public Megastructure prefab;
    [Range(0f, 1f)]
    public float spawnChance;
}
