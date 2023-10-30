using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _radius;
        private Turret[] _turrets;
        private Destructible _target;


        #region Unity Events

        private void Awake()
        {
            _turrets = GetComponentsInChildren<Turret>();
            _target = null;
        }

        private void Update()
        {
            if (_target && _target.isActiveAndEnabled)
            {
                Vector3 targetVector = GetShootingPoint();

                if (targetVector.magnitude <= _radius)
                {
                    foreach (Turret turret in _turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }
                else
                {
                    _target = null;
                }
                
            }
            else
            {
                TryFindTarget();
            }
        }
        #endregion

        private Vector3 GetShootingPoint()
        {
            CircleCollider2D collider = _target.GetComponentInChildren<CircleCollider2D>();
            return collider.bounds.center - transform.position;
        }

        private void TryFindTarget()
        {
            var enter = Physics2D.OverlapCircle(transform.position, _radius);

            if (enter != null && enter.transform.TryGetComponent<Enemy>(out Enemy target))
            {
                _target = target;
            }
        }

        public void UseAsset(TowerAsset asset)
        {
            _radius = asset.Radius;
            foreach (Turret turret in _turrets)
            {
                turret.SetProperty(asset.TurretProperties);
            }
            _spriteRenderer.sprite = asset.Sprite;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
#endif
    }
}
