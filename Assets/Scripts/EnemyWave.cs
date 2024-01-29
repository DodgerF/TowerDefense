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
                onWaveReady?.Invoke();
            }
        }

        private event Action onWaveReady;
        public void Prepare(Action spawnEnemies)
        {
            _prepareTime += Time.time;
            enabled = true;
            onWaveReady += spawnEnemies;
        }

        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            return null;
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumarateSquads()
        {
            yield return (_groups[0].squads[0].Asset, _groups[0].squads[0].Count, 0);
        }

        
    }
}