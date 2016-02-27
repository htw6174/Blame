using UnityEngine;
using System.Collections;

public class DistrictGenerator : MonoBehaviour {

    public int width;
    public int length;

    public float roadWidth;

    public BlockGenerator blockPrefab;

    public BlockGenerator[] blocks;

    void Start()
    {
        float blockWidth = blockPrefab.width * blockPrefab.scale;
        float blockLength = blockPrefab.length * blockPrefab.scale;

        float totalRoadWidth = roadWidth * (width - 1);
        float totalRoadLength = roadWidth * (length - 1);

        float totalWidth = (blockWidth * (width - 1)) + totalRoadWidth;
        float totalLength = (blockLength * (length - 1)) + totalRoadLength;

        float xSpacing = blockPrefab.width * blockPrefab.scale + roadWidth;
        float zSpacing = blockPrefab.length * blockPrefab.scale + roadWidth;
        blocks = new BlockGenerator[width * length];

        for(int i = 0, z = 0; z < length; z++)
        {
            for(int x = 0; x < width; x++, i++)
            {
                float xPos = ((float)x * xSpacing) - ((totalWidth) / 2f);
                float zPos = ((float)z * zSpacing) - (totalLength / 2f);
                Vector3 blockPosition = new Vector3(xPos, 0f, zPos);

                BlockGenerator newBlock = Instantiate(blockPrefab);
                newBlock.transform.SetParent(transform, false);
                newBlock.transform.position = transform.TransformPoint(blockPosition);
            }
        }
    }
}
