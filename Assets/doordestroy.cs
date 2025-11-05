using UnityEngine;

public class doordestroy : MonoBehaviour
{
    public int totalPedestals = 3;
    private int activeCount = 0;
    public float distance = 5f;  // Distance vers le haut
    public float speed = 2f;     // Vitesse du mouvement

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moving = true;
    
    void Start()
    {
        // On définit la position de départ et la position finale
        startPos = transform.position;
        targetPos = startPos + Vector3.up * distance;
    }


    public void PedestalActivated()
    {
        activeCount++;
        Debug.Log($"Piédestal activé ! ({activeCount}/{totalPedestals})");
        CheckDestroy();
    }

    public void PedestalDeactivated()
    {
        activeCount--;
        Debug.Log($"Piédestal désactivé ! ({activeCount}/{totalPedestals})");
    }

    void CheckDestroy()
    {
        if (activeCount >= totalPedestals)
        {
            Debug.Log("🚪 Tous les piédestaux activés : le cube disparaît !");
            // Déplacement progressif vers la cible
            //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            // Quand on est proche de la position finale, on arrête le mouvement
            //if (Vector3.Distance(transform.position, targetPos) < 0.01f) ;
            Destroy(gameObject);
        }
    }
}

