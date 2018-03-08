using UnityEngine;
using System.Collections;

public delegate void ButtonEventHandler();

public class SomeInputTracker : MonoBehaviour {

    public event ButtonEventHandler Button1Press;
    public event ButtonEventHandler Button2Press;

    private void OnButton1()
    {
        if (Button1Press != null)
        {
            Button1Press();
        }
    }

    private void OnButton2()
    {
        if (Button2Press != null)
        {
            Button2Press();
        }
    }

    public void Button1()
    {
        Debug.Log("Button 1 Pressed!");
        OnButton1();
    }

    public void Button2()
    {
        Debug.Log("Button 2 Pressed!");
        OnButton2();
    }
}
