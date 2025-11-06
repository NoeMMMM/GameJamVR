using UnityEngine;

public class LightDesactive : MonoBehaviour
{
    [Header("Assign the player camera")]
    public Camera playerCamera;

    [Header("Settings")]
    [Tooltip("Auto-assign light on this GameObject")]
    public bool autoAssignLight = true;

    [Tooltip("Angle threshold (0 = perpendicular, negative = behind)")]
    public float dotThreshold = 0f;

    [Header("Debug")]
    public bool isBehindCamera = false;
    public bool showDebugLine = true;

    private Light targetLight;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (autoAssignLight)
        {
            targetLight = GetComponent<Light>();
            if (targetLight == null)
            {
                Debug.LogWarning($"No Light component found on {gameObject.name}");
            }
        }
    }

    void Update()
    {
        if (playerCamera == null || targetLight == null)
            return;

        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 toLight = (targetLight.transform.position - playerCamera.transform.position).normalized;
        float dot = Vector3.Dot(cameraForward, toLight);

        isBehindCamera = dot < dotThreshold;
        targetLight.enabled = !isBehindCamera;

        if (showDebugLine)
        {
            Debug.DrawLine(playerCamera.transform.position, targetLight.transform.position, 
                isBehindCamera ? Color.red : Color.green);
        }
    }
}