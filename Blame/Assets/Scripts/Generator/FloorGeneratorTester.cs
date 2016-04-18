using UnityEngine;
using System.Collections;

public class FloorGeneratorTester : Megastructure {

    public int height;

    public Megastructure topSide;
    public Megastructure underSide;

    public GameObject baseCenter;
    public GameObject baseEdge;
    public GameObject baseCorner;

    public GameObject pillarBase;
    public GameObject pillarCenter;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        UpdateDimensions();

        GameObject lowerFloor = Instantiate(baseCenter);
        lowerFloor.transform.SetParent(transform, false);
        lowerFloor.transform.localPosition = Vector3.up * -0.5f;
        lowerFloor.transform.localScale = new Vector3(Width, 1f, Length);

        GameObject upperFloor = Instantiate(baseCenter);
        upperFloor.transform.SetParent(transform, false);
        upperFloor.transform.localPosition = Vector3.up * height;
        upperFloor.transform.localScale = new Vector3(Width, 1f, Length);

        Megastructure topSideStructure = Instantiate(topSide) as Megastructure;
        topSideStructure.transform.SetParent(transform, false);
        topSideStructure.transform.localPosition = Vector3.zero;

        Megastructure underSideStructure = Instantiate(underSide) as Megastructure;
        underSideStructure.transform.SetParent(transform, false);
        underSideStructure.transform.localPosition = Vector3.up * height;
        underSideStructure.transform.eulerAngles = new Vector3(180f, 0f, 0f);
    }

    public void UpdateDimensions()
    {
        Width = Mathf.Max(topSide.Width, underSide.Width);
        Length = Mathf.Max(topSide.Length, underSide.Length);
        height = Mathf.Min(Width, Length);
    }
}
