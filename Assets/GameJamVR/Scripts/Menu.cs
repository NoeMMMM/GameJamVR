using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    private Image buttonImage;
    public Color hoverColor = Color.yellow;
    public Color normalColor = Color.white;

    [Tooltip("Map2")]
    public string sceneToLoad;

    private void Start()
    {
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
            buttonImage.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.1f; // petit effet d’agrandissement
        if (buttonImage != null)
            buttonImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        if (buttonImage != null)
            buttonImage.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Charger la scène (Play, Options, etc.)
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Quitter l’application
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
