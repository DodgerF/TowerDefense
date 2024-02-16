using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _radius;
        [SerializeField] private Turret[] _turrets;

        private Enemy _target;

        private Type _type;

        #region Unity Events

        private void Awake()
        {
            _target = null;
        }

        private void Update()
        {   
            if (_target && _target.isActiveAndEnabled)
            {
                if (Vector3.Distance(_target.transform.position, transform.position)<= _radius)
                {
                    foreach (Turret turret in _turrets)
                    {
                        var targetVector = (Vector3)_target.GetComponent<CircleCollider2D>().offset + _target.transform.position - turret.transform.position;
                        turret.transform.up = targetVector;
                        turret.Fire(targetVector);
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

        private void TryFindTarget()
        {
            var enter = Physics2D.OverlapCircle(transform.position, _radius);

            if (enter != null && enter.transform.TryGetComponent<Enemy>(out Enemy target))
            {
                if (target.Type == _type || _type == Type.All)
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

            _type = asset.Type;
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
