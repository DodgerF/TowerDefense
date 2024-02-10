using System.Collections.Generic;

namespace TowerDefense
{
    public class Freezer : Spell
    {
        private Dictionary<Enemy, float> _enemiesSpeed = new Dictionary<Enemy, float>();
        protected override void Update()
        {
            base.Update();

            if (!_onCooldown && _enemiesSpeed.Count != 0)
            {
                foreach (Enemy enemy in FindObjectsOfType<Enemy>())
                {
                    enemy.MoveSpeed = _enemiesSpeed[enemy]; 
                }
                _enemiesSpeed.Clear();
            }
        }
        public override void Use()
        {
            base.Use();

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                _enemiesSpeed.Add(enemy, enemy.MoveSpeed);
                enemy.MoveSpeed = 0;
            }
        }
    }
}

