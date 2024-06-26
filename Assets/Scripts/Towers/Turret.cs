using SpaceShooter;
using UnityEngine;


namespace TowerDefense
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretProperties _turretProperties;
        public TurretProperties Property => _turretProperties;

        private float _refireTimer;
        public bool IsCanFire => _refireTimer <= 0;


        #region UnityEvents

        private void Update()
        {
            if (_refireTimer > 0)
            {
                _refireTimer -= Time.deltaTime;
            }
        }
        #endregion

        #region Public API
        public void Fire(Vector3 targetVector)
        {
            if (_turretProperties == null) return;
            if (_refireTimer > 0) return;      

            if (_turretProperties.ProjectilePrefab != null)
            {
                //var projectile = MyObjectPool.SpawnObject(_turretProperties.ProjectilePrefab.gameObject, transform.position, Quaternion.identity,
                //                                          MyObjectPool.PoolType.Projectiles);
                var projectile = Instantiate(_turretProperties.ProjectilePrefab.gameObject, transform.position, Quaternion.identity);
                projectile.transform.up = transform.up;
                projectile.GetComponent<Projectile>().Target = targetVector;
            }
            _refireTimer = _turretProperties.RateOfFire;
        }
        public void AssignLoadout(TurretProperties properties)
        {
            if (_turretProperties.Mode != properties.Mode) return;

            _refireTimer = 0;
            _turretProperties = properties;
        }

        public void SetProperty(TurretProperties properties)
        {
            _turretProperties = properties;
        }

        #endregion
    }
}
