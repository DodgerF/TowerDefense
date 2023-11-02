using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Projectile : MonoBehaviour
    {
        #region Properties
        [SerializeField] protected float _velocity;
        public float Velocity => _velocity;
        [SerializeField] protected float _lifetime;
        [SerializeField] protected float _damage;
        protected float _timer;

        protected Vector3 _targetPoint;
        public Vector3 Target { set { _targetPoint = value; } }
        #endregion

        #region Unity Events


        protected virtual void OnEnable()
        {
            _timer = 0f;
        }

        protected virtual void Update()
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
            if (hit)
            {
                if (hit.collider.TryGetComponent<Destructible>(out Destructible destructible))
                {
                    destructible.ApplyDamage(_damage);
                }
                OnProjectileLifeEnd();
            }
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
