using System;
using UnityEngine;
using System.Collections.Generic;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Path[] _paths;
        
        private EnemyWave[] _waves;
        private int _currentWave;

        public event Action OnAllWavesDead;

        private MyList<GameObject> _objects = new MyList<GameObject>(); 
        private void RecordEnemyDeath() {
            for (int i = 0; i < _objects.Count; i++) 
            {
                if (_objects.Get(i).activeSelf) return; 
            }

            if (_currentWave >= _waves.Length)
            {
                OnAllWavesDead?.Invoke();
            }
            else
            {
                ForceNextWave();
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
                    if (_objects.Add(e.gameObject))
                    {
                        e.OnEnd += RecordEnemyDeath;
                    }
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
    public class MyList<T>
    {
        private List<T> _list = new List<T>();
        public int Count { get { return _list.Count; } }
        public bool Add(T obj)
        {
            foreach (T item in _list)
            {
                if (item.Equals(obj)) return false;
            }
            _list.Add(obj);
            return true;
        }
        public T Get(int index)
        {
            return _list[index];
        }
    }
}
