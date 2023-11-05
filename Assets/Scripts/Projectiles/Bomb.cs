using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Explosion))]
    public class Bomb : Projectile
    {
        [SerializeField] private float _radiusExplosion;
        private Explosion _anim;
        private void Awake()
        {
            _anim = GetComponent<Explosion>();
        }
        protected override void DealDamage(RaycastHit2D hit)
        {
            if (!hit) return;
            OnProjectileLifeEnd();
        }

        protected override void OnProjectileLifeEnd()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radiusExplosion);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.ApplyDamage(_damage);
                }
            }
            _anim.BlowUp(transform.position, _radiusExplosion);
            base.OnProjectileLifeEnd();
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