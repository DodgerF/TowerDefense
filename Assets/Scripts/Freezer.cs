using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Freezer : Spell
    {
        private Dictionary<Enemy, float> _enemiesSpeed = new Dictionary<Enemy, float>();
        private float _timeActive;
        private void Awake()
        {
            _timeActive = _asset.Inf[GetLevel()].Time;
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
                _enemiesSpeed.Clear();
            }
        }
        public override void Use()
        {
            if (_onCooldown) return;
            base.Use();

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                _enemiesSpeed.Add(enemy, enemy.MoveSpeed);
                enemy.MoveSpeed = 0;
            }
            _timeActive += Time.time;
        }
    }
}

