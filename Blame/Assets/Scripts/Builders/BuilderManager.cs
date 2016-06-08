using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuilderManager : MonoBehaviour {

    public List<Builder> ActiveBuilders;

    public void AddBuilder(Builder newBuilder)
    {
        ActiveBuilders.Add(newBuilder);
    }

    public void RemoveBuilder(Builder destroyed)
    {
        ActiveBuilders.Remove(destroyed);
    }
}
