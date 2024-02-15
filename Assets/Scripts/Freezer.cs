using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Freezer : Spell
    {
        private Dictionary<Enemy, float> _enemiesSpeed = new Dictionary<Enemy, float>();
        private float _timeActive;
        private float _percent;
        private void Start()
        {
            var level = GetLevel();
            _timeActive = _asset.Inf[level].Time;
            _percent = _asset.Inf[level].Percent;
        }
        protected override void Update()
        {
            base.Update();

            if (_timeActive <= Time.time && _enemiesSpeed.Count != 0)
            {
                foreach (var enemy in _enemiesSpeed)
                {
                    enemy.Key.MoveSpeed = enemy.Value;
                }
                _timeActive = _asset.Inf[GetLevel()].Time;
                EnemySpawner.OnEnemySpawn -= SlowDownEnemy;
                _enemiesSpeed.Clear();
            }
        }
        public override void Use()
        {
            if (_onCooldown) return;
            base.Use();

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                SlowDownEnemy(enemy);
            }

            EnemySpawner.OnEnemySpawn += SlowDownEnemy;

            _timeActive += Time.time;
        }
        private void SlowDownEnemy(Enemy enemy)
        {
            _enemiesSpeed.Add(enemy, enemy.MoveSpeed);
            enemy.MoveSpeed = enemy.MoveSpeed * _percent;
            
        }
    }
}

