using SpaceShooter;
using UnityEngine;


namespace TowerDefense
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretProperties m_TurretProperties;
        [SerializeField] private int m_DefaultCapacity;
        public TurretProperties Property => m_TurretProperties;

        private float m_RefireTimer;

        public bool IsCanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        private ProjectilePool m_Pool;

        #region UnityEvents
        private void Awake()
        {
            m_Pool = new ProjectilePool(m_TurretProperties.ProjectilePrefab, m_DefaultCapacity);
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
            else if (m_TurretProperties.Mode == TurretMode.Auto)
            {
                Fire();
            }
        }
        #endregion

        #region Public API
        public void Fire()
        {
            if (m_TurretProperties == null) return;
            if (m_RefireTimer > 0) return;

            if (m_Ship)
            {
                if (m_Ship.DrawEnergy(m_TurretProperties.EnergeUsage) == false) return;

                if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            }
            

            if (m_TurretProperties.ProjectilePrefab != null)
            {
                Projectile projectile = m_Pool.Get();
                projectile.SetPool(m_Pool);
                projectile.transform.position = transform.position;
                projectile.transform.up = transform.up;
                projectile.SetParentShooter(m_Ship);
            }
            m_RefireTimer = m_TurretProperties.RateOfFire;

            {
                //SFX
            }
        }
        public void AssignLoadout(TurretProperties properties)
        {
            if (m_TurretProperties.Mode != properties.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = properties;
        }

        #endregion
    }
}
