using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Projectile : MonoBehaviour
    {
        #region Properties
        [SerializeField] private float _velocity;
        public float Velocity => _velocity;
        [SerializeField] private float _lifetime;
        [SerializeField] private float _damage;
        private float _timer;

        private Destructible _parent;
        #endregion

        #region Unity Events


        private void OnEnable()
        {
            _timer = 0f;
        }

        private void Update()
        {
            float stepLenght = Time.deltaTime * _velocity;
            Vector2 step = transform.up * stepLenght;

            CheckRaycastAhead(stepLenght);
            CheckTimer();

            transform.position += new Vector3(step.x, step.y, 0);
        }

        #endregion

        private void CheckRaycastAhead(float stepLenght)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);
            if (hit)
            {
                if (hit.collider.TryGetComponent<Destructible>(out Destructible destructible) && destructible != _parent)
                {
                    destructible.ApplyDamage(_damage);
                }
                OnProjectileLifeEnd();
            }
        }

        private void CheckTimer()
        {
            _timer += Time.deltaTime;
            if (_timer > _lifetime)
            {
                OnProjectileLifeEnd();
            }
        }

        private void OnProjectileLifeEnd()
        {
            MyObjectPool.ReturnObjectToPool(gameObject);
        }

        public void SetParentShooter(Destructible parent)
        {
            _parent = parent;
        }
    }
}
