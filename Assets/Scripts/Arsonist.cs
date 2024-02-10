using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Arsonist : Spell
    {
        //TODO: add field "_damage"
        private void Awake()
        {
            
        }
        public override void Use()
        {
            base.Use();

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.ApplyDamage(1);
                //TODO: change this ^ on _damage
            }
        }
    }
}

