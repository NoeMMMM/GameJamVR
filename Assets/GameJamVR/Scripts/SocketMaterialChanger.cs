using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketMaterialChanger : MonoBehaviour
{
    [Header("Matériaux")]
    public Material newMaterial;  // Matériau appliqué quand l’objet est inséré
    private Material originalMaterial; // Pour restaurer après retrait

    private XRSocketInteractor socket;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();

        // On abonne les événements
        socket.selectEntered.AddListener(OnObjectPlaced);
        socket.selectExited.AddListener(OnObjectRemoved);
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        // Récupère le MeshRenderer de l’objet placé
        var renderer = args.interactableObject.transform.GetComponentInChildren<MeshRenderer>();
        if (renderer != null && newMaterial != null)
        {
            originalMaterial = renderer.material;
            renderer.material = newMaterial;
        }
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        // Remet le matériau d’origine
        var renderer = args.interactableObject.transform.GetComponentInChildren<MeshRenderer>();
        if (renderer != null && originalMaterial != null)
        {
            renderer.material = originalMaterial;
        }
    }

    /*private void OnDestroy()
    {
        // Nettoyage
        socket.selectEntered.RemoveListener(OnObjectPlaced);
        socket.selectExited.RemoveListener(OnObjectRemoved);
    }*/
}
