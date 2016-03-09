using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public float speed = 1f;
    public float speedVariance = 0.2f;

    void Start()
    {
        speed += Random.Range(-speedVariance, speedVariance);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
