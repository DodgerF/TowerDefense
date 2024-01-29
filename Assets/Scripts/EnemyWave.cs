using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave : MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyAsset Asset;
            public int Count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        [SerializeField] private PathGroup[] _groups;
        [SerializeField] private float _prepareTime = 10f;

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time >= _prepareTime)
            {
                enabled = false;
                _onWaveReady?.Invoke();
            }
        }

        private event Action _onWaveReady;
        public void Prepare(Action spawnEnemies)
        {
            _prepareTime += Time.time;
            enabled = true;
            _onWaveReady += spawnEnemies;
        }
        public void DisableWave(Action spawnEnemies)
        {
            _onWaveReady -= spawnEnemies;
            enabled = false;
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumarateSquads()
        {
            for (int i = 0; i < _groups.Length; i++)
            {
                foreach (var squad in _groups[i].squads)
                {
                    yield return (squad.Asset, squad.Count, i);
                }
            }
        }

        
    }
}