using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class crossSpinTester : MonoBehaviour {

    public GameObject point1;
    public GameObject point2;
    public GameObject point3;

    public Vector3 circumcenter;

    public Vector3 p1xp2;
    public Vector3 p1xp3;
    public Vector3 lxr;

    private Vector3 p1;
    private Vector3 p2;
    private Vector3 p3;

    void Update()
    {
        p1 = point1.transform.position;
        p2 = point2.transform.position;
        p3 = point3.transform.position;
        circumcenter = FindCircumcenter();
    }

    private void FindSpinOrder()
    {
        Vector3 p1top2 = p2 - p1;
        Vector3 p1top3 = p3 - p1;
        p1xp2 = Vector3.Cross(p1, p2);
        p1xp3 = Vector3.Cross(p1, p3);
        lxr = Vector3.Cross(p1top2, p1top3);
    }

    private Vector3 FindCircumcenter()
    {
        float m1 = (p2.y - p1.y) / (p2.x - p1.x);
        float m2 = (p3.y - p1.y) / (p3.x - p1.x);
        m1 = -1f / m1;
        m2 = -1f / m2;
        Vector3 bisect1 = (p1 + p2) / 2f;
        Vector3 bisect2 = (p1 + p3) / 2f;
        float b1 = bisect1.y - (m1 * bisect1.x);
        float b2 = bisect2.y - (m2 * bisect2.x);

        float x = (b2 - b1) / (m1 - m2);

        float y = (m1 * x) + b1;

        Vector3 pos = new Vector3(x, y, 0f);

        return pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(circumcenter, 1f);
    }
}
