using UnityEngine;
using System.Collections;
using System;

public class Builder : MonoBehaviour {

    public float lifespan;
    private float birthTime;

    public float range;

    private BuilderManager manager;

    void Awake()
    {
        birthTime = Time.time;
        manager = GameObject.FindGameObjectWithTag("BuilderManager").GetComponent<BuilderManager>();
        manager.AddBuilder(this);
    }

    void Update()
    {
        if(Time.time - birthTime > lifespan)
        {
            Kill();
        }
    }

    private void Kill()
    {
        manager.RemoveBuilder(this);
        Destroy(gameObject);
    }
}
