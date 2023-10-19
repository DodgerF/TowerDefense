using UnityEngine;


namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretProperties m_TurretProperties;
        public TurretProperties Property => m_TurretProperties;

        private float m_RefireTimer;

        public bool IsCanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        #region UnityEvents
       
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
                Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
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
