using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Path[] _paths;
        
        private EnemyWave[] _waves;
        private int _currentWave;

        public event Action OnAllWavesDead;

        [SerializeField] private int _activeEnemyCount = 0;
        private int count = 0;
        private void RecordEnemyDeath() {
            print(++count);
            if (--_activeEnemyCount == 0)
            {
                if (_currentWave < _waves.Length  && _waves[_currentWave] )
                {
                    ForceNextWave();
                }
                else
                {
                    print("aboba");
                    OnAllWavesDead?.Invoke();
                }
            }
        }

        private void Awake()
        {
            _waves = GetComponentsInChildren<EnemyWave>();
            _currentWave = 0;
        }
        private void OnEnable()
        {
            EnemyWave.OnWaveReady += SpawnEnemies;
        }
        private void OnDisable()
        {
            EnemyWave.OnWaveReady -= SpawnEnemies;
        }

        private void Start()
        {
            _waves[_currentWave].Prepare();
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
                    _activeEnemyCount++;
                }
            }
            _waves[_currentWave].DisableWave();

            if (++_currentWave < _waves.Length)
            {
                _waves[_currentWave].Prepare();
            }
        }

        public void ForceNextWave()
        {
            if (_currentWave >= _waves.Length) return;

            Player.Instance.SetGold(Player.Instance.Gold + (int)_waves[_currentWave].GetRemainingTime());
            SpawnEnemies();
        }
    }
}
