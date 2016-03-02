using UnityEngine;
using System.Collections;

public class CameraFog : MonoBehaviour {

    public bool fog;

    public Color fogColor;
    public float fogDensity;
    public float fogStartDistance;
    public float fogEndDistance;
    public FogMode fogMode;

    public void SetFog()
    {
        RenderSettings.fog = fog;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogStartDistance = fogStartDistance;
        RenderSettings.fogEndDistance = fogEndDistance;
        RenderSettings.fogMode = fogMode;
    }
}
