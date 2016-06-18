using UnityEngine;
using System.Collections;

[System.Serializable]
public class BlockFace {

    public NodeGrid grid;
    public Vector3 relativePosition;
    public Vector3 facingDirection; //Eventually needed for blocks that can only be placed on certain faces
}
