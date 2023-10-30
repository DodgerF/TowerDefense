using SpaceShooter;
using System;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(CircleArea))]
    public abstract class Spawner : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// Режим спавна
        /// </summary>
        public enum SpawnMode
        {
            Start,
            Loop
        }
        [SerializeField] private SpawnMode _spawnMode;
        [SerializeField] private int _numSpawns;
        [SerializeField] private float _respawnTime;
        private float _timer;
        /// <summary>
        /// Зона спавна
        /// </summary>
        private CircleArea _area;
        protected abstract GameObject GenerateSpawnedEntity();
        #endregion

        #region Unity Events
        private void Awake()
        {
            _area = GetComponent<CircleArea>();
        }

        private void Start()
        {
            if (_spawnMode == SpawnMode.Start)
            {
                Spawn();
            }
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            if (_spawnMode == SpawnMode.Loop && _timer <= 0)
            {
                Spawn();
                _timer = _respawnTime;
            }
        } 
        #endregion

        private void Spawn()
        {
            for (int i = 0; i < _numSpawns; i++)
            {
                var e = GenerateSpawnedEntity();
                e.transform.position = _area.GetRandomInsideZone();
            }
        }
    }
}