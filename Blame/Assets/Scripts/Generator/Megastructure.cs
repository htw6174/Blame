using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for objects in the city
/// </summary>
[System.Serializable]
public class Megastructure : MonoBehaviour {

    //Total dimensions of the object
    [SerializeField]
    private int width;

    [SerializeField]
    private int length;

    public int Width
    {
        get
        {
            return width;
        }

        set
        {
            width = Mathf.Abs(value);
        }
    }

    public int Length
    {
        get
        {
            return length;
        }

        set
        {
            length = Mathf.Abs(value);
        }
    }
}
