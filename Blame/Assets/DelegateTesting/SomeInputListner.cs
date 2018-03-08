using UnityEngine;
using System.Collections;

public class SomeInputListner : MonoBehaviour {

    public SomeInputTracker inputTracker;

    void OnEnable()
    {
        //Tell the delegate in input tracker to run some function
        inputTracker.Button1Press += new ButtonEventHandler(OnButton1Down);
        inputTracker.Button2Press += new ButtonEventHandler(OnButton2Down);
    }

    public void OnButton1Down()
    {
        Debug.Log("Listner saw Button 1 was pressed!");
    }

    public void OnButton2Down()
    {
        Debug.Log("Listner saw Button 2 was pressed!");
    }
}
