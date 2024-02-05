using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(LevelController))]
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool _isCompleted;

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () => { _isCompleted = true; };
        }

        public bool IsComplited 
        { 
            get 
            {
                return _isCompleted; 
            } 
        }
    }
}
