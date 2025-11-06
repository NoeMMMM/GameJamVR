using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Tooltip("Tous les SnapChecker du niveau")]
    public SnapChecker[] snapCheckers;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckAllSockets()
    {
        foreach (var snap in snapCheckers)
        {
            if (!snap.IsCorrect) return;
        }

        OnPuzzleCompleted();
    }

    private void OnPuzzleCompleted()
    {
        Debug.Log("✅ Puzzle terminé !");
        
            // Ici tu peux :
            // - allumer une lumière
            // - jouer un son
            // - déclencher une animation ou un particle effect
        }
    }

