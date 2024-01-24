using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Projectile : MonoBehaviour
    {
        #region Properties
        [SerializeField] protected float _velocity;
        protected float _lifetime;
        [SerializeField] protected float _damage;
        protected float _timer;

        protected Vector3 _targetPoint;
        public Vector3 Target { set { _targetPoint = value; } }

        [SerializeField] protected Type _type;
        #endregion

        #region Unity Events


        protected virtual void OnEnable()
        {
            float dist = Vector3.Distance(transform.position, _targetPoint);
            float vel = _velocity / 2f; //костыль, чтобы projeectiles исчезали в нужной точке
            _lifetime = dist / vel;
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
            enemy.ApplyDamage(_damage);
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
