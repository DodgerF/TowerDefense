using SpaceShooter;
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

        private void OnDisable()
        {
            Explosion();
        }

        #endregion

        #region Override 
        protected override void DealDamage(Enemy enemy)
        {
            OnProjectileLifeEnd();
        }
        #endregion

        private void Explosion()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radiusExplosion);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<Enemy>(out Enemy enemy) && enemy.Type == _type)
                {
                    enemy.ApplyDamage(_damage);
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