using UnityEngine;
using System.Collections;

public class StructureBuilder : Builder {

    public BuildingBlock structurePrefab; //Upgrade to a list of objects eventually
    
    void Start()
    {
        StartCoroutine(Build());
    }

    void OnTriggerEnter(Collider other)
    {
        AddBlockToList(other);
    }

    void OnTriggerExit(Collider other)
    {
        RemoveBlockFromList(other);
    }

    private IEnumerator Build()
    {
        WaitForSeconds buildDelayTime = new WaitForSeconds(buildDelay);
        while (true)
        {
            yield return buildDelayTime;
            if (blocksInRange.Count > 0)
            {
                BlockFace surfaceCandidate = FindBuildSurface();
                PlaceBlock(surfaceCandidate);
            }
            else
            {
                //Try to move and find new blocks
                Debug.Log("Couldn't find any blocks in range!");
            }
        }
    }

    private BlockFace FindBuildSurface()
    {
        int index = Random.Range(0, blocksInRange.Count);
        return blocksInRange[index].GetRandomFace();
    }

    private void PlaceBlock(BlockFace face)
    {
        BlockFace prefabFace = GetOppositeFace(face.facingDirection); //For now just gets the opposite face on the prefab block, will find better solution
        if (face.grid.GetNode(0, 0)) //If the NodeGrids match up
        {
            Vector3 placedPosition = face.grid.transform.position - prefabFace.grid.transform.localPosition;
            GameObject newStructure = Instantiate(structurePrefab, placedPosition, Quaternion.identity) as GameObject;
        }
    }

    private BlockFace GetOppositeFace(Vector3 facingDirection)
    {
        return structurePrefab.GetOppositeFace(facingDirection);
    }
}
