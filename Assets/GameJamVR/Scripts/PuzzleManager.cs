using UnityEngine;
using System.Collections;


public class PuzzleManager : MonoBehaviour
{
public static PuzzleManager Instance;

[Tooltip("Tous les SnapChecker du niveau")]
public SnapChecker[] snapCheckers;

[Header("Effets de victoire")]
public AudioSource VictorySound;

[Header("Animal à animer")]
public Animator animalAnimator;     // → Animator de ton prefab animal
public GameObject animalObject;     // → GameObject complet de ton animal
public float escapeSpeed = 3f;      // → vitesse de fuite
public float escapeDuration = 2.5f; // → durée de la fuite avant disparition

private bool puzzleCompleted = false;

private void Awake()
{
    Instance = this;
}

public void CheckAllSockets()
{
    if (puzzleCompleted) return;

    foreach (var snap in snapCheckers)
    {
        if (!snap.IsCorrect) return; // si un snap est faux, on quitte
    }

    // Si on arrive ici, tout est correct
    OnPuzzleCompleted();
}

private void OnPuzzleCompleted()
{
    puzzleCompleted = true;

    //Joue le son de victoire s’il existe
    if (VictorySound != null)
        VictorySound.Play();

    //Lance la fuite de l’animal
    if (animalAnimator != null)
        StartCoroutine(AnimalEscape());
}

private IEnumerator AnimalEscape()
{
    Debug.Log(" L’animal prend la fuite !");

    //Lance l’animation "Run" si elle existe dans l’Animator
    animalAnimator.SetTrigger("Run");

    //Fait bouger l’animal vers l’avant pendant quelques secondes
    float timer = 0f;
    Vector3 direction = animalObject.transform.forward;

    while (timer < escapeDuration)
    {
        animalObject.transform.position += direction * escapeSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        yield return null;
    }

    //Fais disparaître l’animal après la fuite
    Destroy(animalObject);
}
}

