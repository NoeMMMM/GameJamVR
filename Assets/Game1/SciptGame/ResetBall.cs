using UnityEngine;
using UnityEngine.UI;

public class ResetBall : MonoBehaviour

{
    [SerializeField] private GameObject _Broball;
    [SerializeField] private Transform[] _SpawnerPoint;
    private int _SpawnIndex = 0;
    // Update is called once per frame
   public void Spawn()
   {
        Instantiate(_Broball, _SpawnerPoint[_SpawnIndex].position,_SpawnerPoint[_SpawnIndex].rotation);
   }
}
