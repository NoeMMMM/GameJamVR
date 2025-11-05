using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        PlayerCollect.OnTargetCollected += UpdateScore;
    }

    private void OnDisable()
    {
        PlayerCollect.OnTargetCollected -= UpdateScore;
    }


    private void Star()
    {
        UpdateScore(0);
    }
    public void UpdateScore(int newScore)
    {
        _scoreText.text = " " + newScore.ToString();
        _scoreText.text = $"Score : {newScore.ToString()}";

    }
}
