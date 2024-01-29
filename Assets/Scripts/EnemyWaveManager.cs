using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Path[] _paths;
        [SerializeField] private EnemyWave _currentWave;


        private void Start()
        { 
            _currentWave.Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            foreach( (EnemyAsset asset, int count, int pathIndex) in _currentWave.EnumarateSquads())
            {
                if (pathIndex > _paths.Length)
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                    return;
                }

                for (int i = 0; i < count; i++)
                {
                    var e = MyObjectPool.SpawnObject(_enemyPrefab.gameObject, transform.position, Quaternion.identity, MyObjectPool.PoolType.Enemies).GetComponent<Enemy>();
                    e.UseAsset(asset);
                    e.GetComponent<EnemyController>().SetPath(_paths[pathIndex]);
                }
            }

            _currentWave = _currentWave.PrepareNext(SpawnEnemies);
        }
    }
}
