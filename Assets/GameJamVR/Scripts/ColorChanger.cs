using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer[] renderers;
    private Color originalColor;

    void Awake()
    {
        // Get all renderers in this object and its children
        renderers = GetComponentsInChildren<MeshRenderer>();
        if (renderers.Length > 0)
            originalColor = renderers[0].material.color;
    }

    public void ChangeColor(Color newColor)
    {
        foreach (MeshRenderer r in renderers)
        {
            r.material.color = newColor;
        }
    }

    public void ResetColor()
    {
        foreach (MeshRenderer r in renderers)
        {
            r.material.color = originalColor;
        }
    }
}
