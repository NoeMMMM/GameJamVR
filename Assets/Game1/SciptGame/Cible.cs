using UnityEngine;

public class Cible : MonoBehaviour
{
    [SerializeField] private int _targetValue = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerCollect>() != null)
        {
            other.gameObject.GetComponent<PlayerCollect>().UpdateScore(_targetValue);
        }
    }
}
