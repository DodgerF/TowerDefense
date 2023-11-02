using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Bomb : Projectile
    {
        [SerializeField] private float _radiusExplosion;


        protected override void OnEnable()
        {
            base.OnEnable();
        }
        protected override void Update()
        {   
            if (Vector3.Distance(transform.position, _targetPoint) < 0.1f)
            {
                OnProjectileLifeEnd();
            }
            base.Update();
        }
        protected override void OnProjectileLifeEnd()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radiusExplosion);

            if (colliders.Length > 0)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.ApplyDamage(_damage);
                    }
                }
            }
            base.OnProjectileLifeEnd();
        }
    }
}