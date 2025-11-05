using UnityEngine;
using System;
public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private ScoreDatas _scoreData; 
    [SerializeField] private UIController _uiController;

    public static Action<int> OnTargetCollected;

    public void UpdateScore(int value)
    {
        _scoreData.ScoreValue = Mathf.Clamp(_scoreData.ScoreValue+value,min:0,max:_scoreData.ScoreValue+value); 
        _uiController.UpdateScore(_scoreData.ScoreValue);
        OnTargetCollected?.Invoke(_scoreData.ScoreValue);
    }
}
