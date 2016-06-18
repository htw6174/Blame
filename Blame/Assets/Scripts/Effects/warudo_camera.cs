using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class warudo_camera : MonoBehaviour {

    public float animationDuration = 1.25f;

    [Range(0f, 10f)]
    public float maxRadius = 2.5f;

    [Range(0f, 1f)]
    public float radius;

    public Vector3 position;

    public Material material;

    private Camera mainCamera;

    void OnEnable()
    {
        mainCamera = GetComponent<Camera>();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(position);
        //Debug.Log(screenPosition);
        screenPosition.y = 1f - screenPosition.y;
        material.SetFloat("_Radius", radius);
        material.SetVector("_Origin", screenPosition);
        Graphics.Blit(src, dst, material);
    }

    [BitStrap.Button]
    public void TimeStop()
    {
        StartCoroutine(AnimateMaterial());
    }

    private IEnumerator AnimateMaterial()
    {
        WaitForSeconds frameTime = new WaitForSeconds(1f / 60f);

        for (int f = 0; f < animationDuration * 60; f++)
        {
            radius = Mathf.Sin((f * Mathf.PI) / (animationDuration  * 59)) * maxRadius;
            radius = Mathf.Clamp(radius, 0f, maxRadius);
            radius = Mathf.Pow(radius, 3f);
            yield return frameTime;
        }
    }
}
