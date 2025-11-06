using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections.Generic;

public class SocketMaterialChanger1 : MonoBehaviour
{
    [Header("Matériaux")]
    public Material newMaterial;  // Matériau appliqué quand l’objet est inséré

    private XRSocketInteractor socket;
    private readonly Dictionary<Transform, Material[]> originalMaterials = new();

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();

        // Abonnement aux événements
        socket.selectEntered.AddListener(OnObjectPlaced);
        socket.selectExited.AddListener(OnObjectRemoved);
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        var target = args.interactableObject.transform;

        // Récupère tous les MeshRenderers dans l'objet et ses enfants
        var renderers = target.GetComponentsInChildren<MeshRenderer>();
        if (renderers.Length == 0 || newMaterial == null)
            return;

        // Sauvegarde les matériaux originaux pour chaque renderer
        Material[] originals = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originals[i] = renderers[i].material;
            renderers[i].material = newMaterial;
        }
        originalMaterials[target] = originals;
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        var target = args.interactableObject.transform;

        // Restaure les matériaux d'origine si on les avait stockés
        if (originalMaterials.TryGetValue(target, out var originals))
        {
            var renderers = target.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length && i < originals.Length; i++)
            {
                renderers[i].material = originals[i];
            }

            originalMaterials.Remove(target);
        }
    }

    private void OnDestroy()
    {
        // Nettoyage
        socket.selectEntered.RemoveListener(OnObjectPlaced);
        socket.selectExited.RemoveListener(OnObjectRemoved);
    }
}
