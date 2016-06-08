using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class NodeGrid : MonoBehaviour {

    public float gridSpacing = 1f / 12f; //Should change this to a static property

    [Range(0, 100)][SerializeField]
    public int gridWidth;
    [Range(0, 100)][SerializeField]
    public int gridHeight;

    [SerializeField, HideInInspector]
    public bool[] nodes;

    public float LeftToRightHalfDist
    {
        get
        {
            return (gridWidth - 1) * gridSpacing * 0.5f;
        }
    }

    public float TopToBottomHalfDist
    {
        get
        {
            return (gridHeight - 1) * gridSpacing * 0.5f;
        }
    }

    public bool getNode(int x, int y)
    {
        return nodes[x + (y * gridWidth)];
    }

    public void setNode(int x, int y, bool value)
    {
        nodes[x + (y * gridWidth)] = value;
    }

    public void flipNode(int x, int y)
    {
        bool original = getNode(x, y);
        setNode(x, y, !original);
    }

    public void InitializeGrid()
    {
        nodes = new bool[gridWidth * gridHeight];
    }

    public void SetAllFalse()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                setNode(x, y, false);
            }
        }
    }

    public void SetAllTrue()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                setNode(x, y, true);
            }
        }
    }

    void OnDrawGizmos()
    {
        Vector3 position = transform.localPosition;
        Gizmos.color = new Color((position.x + 1f) * 0.5f, (position.y + 1f) * 0.5f, (position.z + 1f) *0.5f);
        Vector3 transformedSize = transform.TransformVector(new Vector3(LeftToRightHalfDist * 2f, TopToBottomHalfDist * 2f, 0.1f));
        Gizmos.DrawCube(transform.position, new Vector3(Mathf.Abs(transformedSize.x), Mathf.Abs(transformedSize.y), Mathf.Abs(transformedSize.z)));
    }
}