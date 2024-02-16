using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Projectile : MonoBehaviour
    {
        #region Properties
        [SerializeField] protected float _velocity;
        [SerializeField] protected float _lifetime = 4f;
        [SerializeField] protected float _damage;
        protected float _timer;
        [SerializeField] protected DamageType _damageType;

        protected Vector3 _targetPoint;
        public Vector3 Target { set { _targetPoint = value; } }

        [SerializeField] protected Type _type;
        #endregion

        #region Unity Events


        protected virtual void OnEnable()
        {
            _timer = 0f;
        }

        protected virtual void FixedUpdate()
        {
            float stepLenght = Time.deltaTime * _velocity;
            Vector2 step = transform.up * stepLenght;

            CheckRaycastAhead(stepLenght);
            CheckTimer();

            transform.position += new Vector3(step.x, step.y, 0);
        }

        #endregion

        protected virtual void CheckRaycastAhead(float stepLenght)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);
            CheckHit(hit);
        }

        protected virtual void CheckHit(RaycastHit2D hit)
        {
            if (!hit) return;

            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy) && (enemy.Type == _type || _type == Type.All))
            {
                DealDamage(enemy);
                OnProjectileLifeEnd();
            }
        }

        protected virtual void DealDamage(Enemy enemy)
        {
            enemy.TakeDamageWithArmor(_damage, _damageType);
        }

        protected virtual void CheckTimer()
        {
            _timer += Time.deltaTime;
            if (_timer > _lifetime)
            {
                OnProjectileLifeEnd();
            }
        }

        protected virtual void OnProjectileLifeEnd()
        {
            MyObjectPool.ReturnObjectToPool(gameObject);
        }

    }
}
