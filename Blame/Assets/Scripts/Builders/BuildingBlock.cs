using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BuildingBlock : MonoBehaviour {

    public Vector3 dimensions;

    public NodeGrid front;
    public NodeGrid back;
    public NodeGrid right;
    public NodeGrid left;
    public NodeGrid top;
    public NodeGrid bottom;

    public void SetAllGridsTrue()
    {
        front.SetAllTrue();
        back.SetAllTrue();
        right.SetAllTrue();
        left.SetAllTrue();
        top.SetAllTrue();
        bottom.SetAllTrue();
    }

    public void SetAllGridsFalse()
    {
        front.SetAllFalse();
        back.SetAllFalse();
        right.SetAllFalse();
        left.SetAllFalse();
        top.SetAllFalse();
        bottom.SetAllFalse();
    }

    public void UpdateGrid()
    {
        SetGridPositions();
        SetGridDimensions();
    }

    private void SetGridPositions()
    {
        front.transform.localPosition = new Vector3(0f, 0f, dimensions.z * 0.5f);
        back.transform.localPosition = new Vector3(0f, 0f, -dimensions.z * 0.5f);
        right.transform.localPosition = new Vector3(dimensions.x * 0.5f, 0f, 0f);
        left.transform.localPosition = new Vector3(-dimensions.x * 0.5f, 0f, 0f);
        top.transform.localPosition = new Vector3(0f, dimensions.y * 0.5f, 0f);
        bottom.transform.localPosition = new Vector3(0f, -dimensions.y * 0.5f, 0f);
    }

    private void SetGridDimensions()
    {
        front.gridWidth = (int)(dimensions.x * 12);
        front.gridHeight = (int)(dimensions.y * 12);
        front.InitializeGrid();
        back.gridWidth = (int)(dimensions.x * 12);
        back.gridHeight = (int)(dimensions.y * 12);
        back.InitializeGrid();

        right.gridWidth = (int)(dimensions.z * 12);
        right.gridHeight = (int)(dimensions.y * 12);
        right.InitializeGrid();
        left.gridWidth = (int)(dimensions.z * 12);
        left.gridHeight = (int)(dimensions.y * 12);
        left.InitializeGrid();

        top.gridWidth = (int)(dimensions.x * 12);
        top.gridHeight = (int)(dimensions.z * 12);
        top.InitializeGrid();
        bottom.gridWidth = (int)(dimensions.x * 12);
        bottom.gridHeight = (int)(dimensions.z * 12);
        bottom.InitializeGrid();
    }
}
