using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class NodeGrid : MonoBehaviour {


    [Range(0, 100)][SerializeField]
    public int gridWidth;
    [Range(0, 100)][SerializeField]
    public int gridHeight;

    [SerializeField, HideInInspector]
    private bool[] _nodes = new bool[0];

    private float _nodeSpacing = NodeProperties.nodeSpacing;

    private bool _freeSpace;

    public float nodeSpacing
    {
        get
        {
            return _nodeSpacing;
        }
    }

    public float LeftToRightHalfDist
    {
        get
        {
            return (gridWidth - 1) * nodeSpacing * 0.5f;
        }
    }

    public float TopToBottomHalfDist
    {
        get
        {
            return (gridHeight - 1) * nodeSpacing * 0.5f;
        }
    }

    /// <summary>
    /// Is the grid entirely false?
    /// </summary>
    public bool freeSpace
    {
        get
        {
            return _freeSpace;
        }
        private set
        {
            _freeSpace = freeSpace;
        }
    }

    void OnEnable()
    {
        if (NodesDefined() == false)
        {
            InitializeGrid();
        }
    }

    public Vector3 GetNodePosition(int x, int y)
    {
        return transform.TransformPoint((x * nodeSpacing) - LeftToRightHalfDist, (y * nodeSpacing) - TopToBottomHalfDist, 0f);
    }

    /// <summary>
    /// Returns node position, adjusted backwards (-z) by the node spacing value. Use this when raycasting from nodes.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Vector3 GetNodePositionSetback(int x, int y)
    {
        return transform.TransformPoint((x * nodeSpacing) - LeftToRightHalfDist, (y * nodeSpacing) - TopToBottomHalfDist, -nodeSpacing);
    }

    public bool NodesDefined()
    {
        return _nodes.Length == 0 ? false : true;
    }

    public bool GetNode(int x, int y)
    {
        if (NodesDefined() == false)
        {
            InitializeGrid();
        }
        return _nodes[x + (y * gridWidth)];
    }

    public void SetNode(int x, int y, bool value)
    {
        if (NodesDefined() == false)
        {
            InitializeGrid();
        }
        _nodes[x + (y * gridWidth)] = value;
    }

    public void FlipNode(int x, int y)
    {
        bool original = GetNode(x, y);
        SetNode(x, y, !original);
    }

    private void CheckForFreeSpace()
    {
        bool isFreeSpace = false;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (GetNode(x, y))
                {
                    isFreeSpace = true;
                }
            }
        }
        freeSpace = isFreeSpace;
    }

    public void InitializeGrid()
    {
        _nodes = new bool[gridWidth * gridHeight];
    }

    public void SetAllFalse()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                SetNode(x, y, false);
            }
        }
    }

    public void SetAllTrue()
    {
        //Debug.Log("Setting all true");
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                SetNode(x, y, true);
            }
        }
    }

    /// <summary>
    /// Raycasts from each node to see if there is a collider obstructing it, and sets it false if so
    /// </summary>
    public void CheckForClearance(bool repeat = true)
    {
        Vector3 forward = transform.forward;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (GetNode(x, y))
                {
                    RaycastHit hit;
                    Ray normal = new Ray(GetNodePositionSetback(x, y), forward);
                    if (Physics.Raycast(normal, out hit, nodeSpacing * 2f))
                    {
                        if (hit.transform.tag == Tags.BuildingBlock)
                        {
                            SetNode(x, y, false);
                            //SendMessage to hit block to check its own clearance
                            if (repeat)
                            {
                                hit.transform.SendMessage("CheckForClearanceDontRepeat");
                            }
                        }
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 position = transform.localPosition;
        Gizmos.color = new Color((position.x + 1f) * 0.5f, (position.y + 1f) * 0.5f, (position.z + 1f) *0.5f);
        Vector3 transformedSize = transform.TransformVector(new Vector3(LeftToRightHalfDist * 2f, TopToBottomHalfDist * 2f, 0.1f));
        Gizmos.DrawCube(transform.position, new Vector3(Mathf.Abs(transformedSize.x), Mathf.Abs(transformedSize.y), Mathf.Abs(transformedSize.z)));

        //Vector3 forward = transform.forward;
        //for (int x = 0; x < gridWidth; x++)
        //{
        //    for (int y = 0; y < gridHeight; y++)
        //    {
        //        Ray normal = new Ray(GetNodePosition(x, y), forward);
        //        Gizmos.DrawRay(normal);
        //    }
        //}
    }
}