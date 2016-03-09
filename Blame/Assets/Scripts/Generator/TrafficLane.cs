using UnityEngine;
using System.Collections;

public class TrafficLane : MonoBehaviour {

    public Vector3 size;
    public float speed;
    public int count;
    [Range(0f, 1f)]
    public float density;


    public GameObject carPrefab;
    public GameObject[] cars;

    void Start()
    {
        int carCount = (int)(size.x * size.y * size.z * density);
        cars = new GameObject[count];
        InitializeCarPositions();
    }

    void Update()
    {
        CheckPositions();
    }

    private void InitializeCarPositions()
    {
        for(int i = 0; i < cars.Length; i++)
        {
            Vector3 newPosition = new Vector3(Random.Range(-size.x / 2f, size.x / 2f), Random.Range(-size.y / 2f, size.y / 2f), Random.Range(-size.z / 2f, size.z / 2f));
            GameObject newCar = Instantiate(carPrefab);
            newCar.transform.parent = transform;
            newCar.transform.localPosition = newPosition;
            cars[i] = newCar;
        }
    }

    private void CheckPositions()
    {
        for(int i = 0; i < cars.Length; i++)
        {
            Vector3 carPos = cars[i].transform.localPosition;
            if(carPos.z > size.z / 2f)
            {
                cars[i].transform.localPosition = new Vector3(carPos.x, carPos.y, -size.z / 2f);
            }
        }
    }
}
