using UnityEngine;
using System.Collections.Generic;

public class LightManager : MonoBehaviour
{
    [Header("Camera Settings")]
    [Tooltip("Leave empty to auto-find Main Camera")]
    public Camera playerCamera;

    [Header("Light Settings")]
    [Tooltip("Parent object containing all lights (leave empty to search entire scene)")]
    public Transform lightsParent;

    [Tooltip("Auto-find all spot lights in scene on Start")]
    public bool autoFindLights = true;

    [Header("Behavior Settings")]
    [Tooltip("Angle threshold to consider light behind camera (0 = perpendicular, negative = behind)")]
    public float dotThreshold = 0f;

    [Header("Debug")]
    public bool showDebugLines = true;
    public int activeLightsCount = 0;
    public int totalLightsCount = 0;

    private Light[] spotLights;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            if (playerCamera == null)
            {
                Debug.LogError("No player camera found! Please assign it manually.");
            }
        }

        if (autoFindLights)
        {
            FindAllSpotLights();
        }
    }

    public void FindAllSpotLights()
    {
        Light[] allLights;

        if (lightsParent != null)
        {
            allLights = lightsParent.GetComponentsInChildren<Light>();
        }
        else
        {
            allLights = FindObjectsByType<Light>(FindObjectsSortMode.None);
        }

        List<Light> spotLightsList = new List<Light>();

        foreach (Light light in allLights)
        {
            if (light != null && IsSpotLight(light))
            {
                spotLightsList.Add(light);
            }
        }

        spotLights = spotLightsList.ToArray();
        totalLightsCount = spotLights.Length;
        Debug.Log($"Found {totalLightsCount} spot lights in the scene.");
    }

    private bool IsSpotLight(Light light)
    {
        return light.name.Contains("Spot");
    }

    void Update()
    {
        if (playerCamera == null || spotLights == null || spotLights.Length == 0)
            return;

        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraPosition = playerCamera.transform.position;

        activeLightsCount = 0;

        foreach (Light spotLight in spotLights)
        {
            if (spotLight == null)
                continue;

            Vector3 toLight = (spotLight.transform.position - cameraPosition).normalized;
            float dot = Vector3.Dot(cameraForward, toLight);

            bool isBehindCamera = dot < dotThreshold;

            spotLight.enabled = !isBehindCamera;

            if (spotLight.enabled)
            {
                activeLightsCount++;
            }

            if (showDebugLines)
            {
                Debug.DrawLine(cameraPosition, spotLight.transform.position, 
                    isBehindCamera ? Color.red : Color.green);
            }
        }
    }
}
