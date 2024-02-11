
namespace TowerDefense
{
    public class Ignite : Spell
    {
        private float _damage;
        private void Awake()
        {
            _damage = _asset.Inf[Upgrades.GetUpgradeLevel(_asset)].Damage;
        }
        public override void Use()
        {
            base.Use();

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.ApplyDamage(_damage);
            }
        }
    }
}

