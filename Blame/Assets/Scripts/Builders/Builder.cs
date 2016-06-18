using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Builder : MonoBehaviour {

    public int lifespan = 1;
    private float birthTime;

    public float range = 10f;
    public float buildDelay = 0.5f;

    [HideInInspector]
    public SphereCollider rangeCollider;

    public List<BuildingBlock> blocksInRange;

    private BuilderManager manager;

    void OnEnable()
    {
        birthTime = Time.time;

        manager = GameObject.FindGameObjectWithTag("BuilderManager").GetComponent<BuilderManager>();
        manager.AddBuilder(this);

        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = range;
    }

    void Update()
    {
        if(Time.time - birthTime > lifespan)
        {
            Kill();
        }
    }

    /// <summary>
    /// Call in OnTriggerEnter to check for new blocks in range
    /// </summary>
    /// <param name="other"></param>
    public void AddBlockToList(Collider other)
    {
        if (other.tag == Tags.BuildingBlock)
        {
            blocksInRange.Add(other.GetComponent<BuildingBlock>());
        }
    }

    /// <summary>
    /// Call in OnTriggerExit to remove old blocks from blocksInRange
    /// </summary>
    /// <param name="other"></param>
    public void RemoveBlockFromList(Collider other)
    {
        if (other.tag == Tags.BuildingBlock)
        {
            blocksInRange.Remove(other.GetComponent<BuildingBlock>());
        }
    }

    private void Kill()
    {
        manager.RemoveBuilder(this);
        Destroy(gameObject);
    }
}
