using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Path[] _paths;
        
        private EnemyWave[] _waves;
        private int _currentWave;

        private void Awake()
        {
            _waves = GetComponentsInChildren<EnemyWave>();
            _currentWave = 0;
        }

        private void Start()
        {
            _waves[_currentWave].Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            foreach( (EnemyAsset asset, int count, int pathIndex) in _waves[_currentWave].EnumarateSquads())
            {
                if (pathIndex > _paths.Length)
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                    return;
                }

                for (int i = 0; i < count; i++)
                {
                    var e = MyObjectPool.SpawnObject(_enemyPrefab.gameObject, _paths[pathIndex].StartArea.GetRandomInsideZone() ,
                        Quaternion.identity, MyObjectPool.PoolType.Enemies).GetComponent<Enemy>();
                    e.UseAsset(asset);
                    e.GetComponent<EnemyController>().SetPath(_paths[pathIndex]);
                }
            }
            _waves[_currentWave].DisableWave(SpawnEnemies);

            _currentWave++;
            if (_currentWave < _waves.Length)
            {
                _waves[_currentWave].Prepare(SpawnEnemies);
            }
        }
    }
}
