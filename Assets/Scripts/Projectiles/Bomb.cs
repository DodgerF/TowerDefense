using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Explosion))]
    public class Bomb : Projectile
    {
        #region Fields

        [SerializeField] private float _radiusExplosion;
        private Explosion _anim;

        #endregion

        #region Unity Events
        private void Awake()
        {
            _anim = GetComponent<Explosion>();
        }

        #endregion

        #region Override 
        protected override void DealDamage(Enemy enemy)
        {
            Explosion();
            OnProjectileLifeEnd();
        }
        protected override void CheckTimer()
        {
            _timer += Time.deltaTime;
            if (_timer > _lifetime)
            {
                print(_timer);
                Explosion();
                OnProjectileLifeEnd();
            }
        }
        #endregion

        private void Explosion()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radiusExplosion);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<Enemy>(out var enemy) && enemy.Type == _type)
                {
                    enemy.TakeDamageWithArmor(_damage, _damageType);
                }
            }
            _anim.BlowUp(transform.position, _radiusExplosion);
        }

        #if UNITY_EDITOR
        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, _radiusExplosion);
        }
        #endif
    }
}