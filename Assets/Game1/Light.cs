using UnityEngine;

public class Light : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Renderer pedestalRenderer;
    [SerializeField] private UnityEngine.Light pedestalLight;
    [SerializeField] private doordestroy doorManager;

    [Header("Objet autorisé")]
    [SerializeField] private GameObject validObject;

    [Header("Couleurs")]
    [SerializeField] private Color offColor = Color.gray;
    [SerializeField] private Color onColor = Color.yellow;

    private bool isActivated = false;

    private void Start()
    {
        if (pedestalRenderer != null)
            pedestalRenderer.material.color = offColor;

        if (pedestalLight != null)
            pedestalLight.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == validObject && !isActivated)
        {
            if (pedestalRenderer != null)
                pedestalRenderer.material.color = onColor;

            if (pedestalLight != null)
                pedestalLight.enabled = true;

            if (doorManager != null)
                doorManager.PedestalActivated();

            isActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == validObject && isActivated)
        {
            if (pedestalRenderer != null)
                pedestalRenderer.material.color = offColor;

            if (pedestalLight != null)
                pedestalLight.enabled = false;

            if (doorManager != null)
                doorManager.PedestalDeactivated();

            isActivated = false;
        }
    }
}
