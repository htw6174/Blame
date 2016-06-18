using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BuildingBlock : MonoBehaviour {

    public Vector3 dimensions;

    public BlockFace[] faces;

    private int nodesPerWorldUnit = NodeProperties.nodesPerWorldUnit;

    void OnEnable()
    {
        CheckForClearance();
    }

    public BlockFace GetRandomFace()
    {
        int index = Random.Range(0, faces.Length);
        return faces[index];
    }

    public BlockFace GetOppositeFace(Vector3 facingDirection)
    {
        foreach (BlockFace face in faces)
        {
            if (face.facingDirection + facingDirection == Vector3.zero)
            {
                return face;
            }
        }
        return null;
    }

    public void SetAllGridsTrue()
    {
        foreach (BlockFace face in faces)
        {
            face.grid.SetAllTrue();
        }
    }

    public void SetAllGridsFalse()
    {
        foreach (BlockFace face in faces)
        {
            face.grid.SetAllFalse();
        }
    }

    public void CheckForClearance()
    {
        foreach (BlockFace face in faces)
        {
            face.grid.CheckForClearance();
        }
    }

    public void CheckForClearanceDontRepeat()
    {
        foreach (BlockFace face in faces)
        {
            face.grid.CheckForClearance(false);
        }
    }

    public void UpdateGrid()
    {
        SetGridPositions();
        SetGridDimensions();
    }

    private void SetGridPositions()
    {
        foreach (BlockFace face in faces)
        {
            Vector3 position = new Vector3(face.relativePosition.x * dimensions.x, face.relativePosition.y * dimensions.y, face.relativePosition.z * dimensions.z) * 0.5f;
            face.grid.transform.localPosition = position;
        }
    }

    private void SetGridDimensions()
    {
        foreach (BlockFace face in faces)
        {
            Vector3 widthHeightRotated = face.grid.transform.TransformDirection(dimensions);
            int newWidth = (int)Mathf.Round(Mathf.Abs(widthHeightRotated.x) * nodesPerWorldUnit);
            int newHeight = (int)Mathf.Round(Mathf.Abs(widthHeightRotated.y) * nodesPerWorldUnit);
            face.grid.gridWidth = newWidth;
            face.grid.gridHeight = newHeight;

            face.grid.InitializeGrid();
        }
    }
}
