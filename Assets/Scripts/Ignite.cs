using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Ignite : Spell
    {
        [SerializeField] private Image _circle;
        [SerializeField] private Explosion _anim;
        private float _damage;
        private float _radius;
        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(_asset);

            _damage = _asset.Inf[level].Damage;
            _radius = _asset.Inf[level].Radius;
        }
        

        public override void Use()
        {
            if (_onCooldown) return;
            base.Use();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamageWithArmor(_damage, DamageType.Fire);
                }
            }
            _anim.BlowUp(transform.position, _radius);
        }
    }
}

