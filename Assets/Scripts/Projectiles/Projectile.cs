using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Projectile : Entity
    {
        #region Properties
        [SerializeField] protected float m_Velocity;
        public float Velocity => m_Velocity;
        [SerializeField] protected float m_Lifetime;
        [SerializeField] protected float m_Damage;
        protected float m_Timer;
        private ProjectilePool m_Pool;

        protected Destructible m_Parent;
        #endregion

        #region Unity Events
        private void OnEnable()
        {
            m_Timer = 0f;
        }
        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
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
                if (hit.collider.transform.root.TryGetComponent<Destructible>(out Destructible destructible) && destructible != m_Parent)
                {
                    destructible.ApplyDamage(m_Damage);

                    UpdateScore(destructible);
                }
                OnProjectileLifeEnd();
            }
        }

        protected void UpdateScore(Destructible destructible)
        {
            return;
            /*Player.Instance.AddScore(destructible.ScoreValue);

            if (destructible.TryGetComponent<SpaceShip>(out SpaceShip ship))
            {
                if (ship.HitPoints <= 0)
                {
                    Player.Instance.AddKill();
                }
            }*/
        }
        protected void CheckTimer()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
            {
                OnProjectileLifeEnd();
            }
        }

        protected virtual void OnProjectileLifeEnd()
        {
            m_Pool.Return(this);
        }

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
        public void SetPool(ProjectilePool pool)
        {
            m_Pool = pool;
        }
    }
}