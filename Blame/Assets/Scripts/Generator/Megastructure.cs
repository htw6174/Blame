using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for objects in the city
/// </summary>
public class Megastructure : MonoBehaviour {

    //Total dimensions of the object
    public float width;

    public float length;

    public float Width
    {
        get
        {
            return width;
        }

        set
        {
            width = value;
        }
    }

    public float Length
    {
        get
        {
            return length;
        }

        set
        {
            length = value;
        }
    }
}
