using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public enum GameStatus
    {
        Common,
        Aim
    }
    [RequireComponent(typeof(Explosion))]
    public class Ignite : Spell
    {
        private GameObject _circle;
        private Explosion _anim;
        //TODO: убрать в другой класс
        private GameStatus _status;
        private float _damage;
        private float _radius;

        private void Awake()
        {
            
        }

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(_asset);

            _anim = GetComponent<Explosion>();
            _damage = _asset.Inf[level].Damage;
            _radius = _asset.Inf[level].Radius;
            _status = GameStatus.Common;
        }

        protected override void Update()
        {
            if (_status == GameStatus.Aim)
            {
                SetCirclePos();

                if (Input.GetMouseButtonDown(0))
                {
                    BlowUp();
                    base.Use();
                    _status = GameStatus.Common;
                    print(GameStatus.Common);
                }
            }
            base.Update();
        }

        public void SetCirclePos()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            _circle.transform.position = mousePos;
        }

        public override void Use()
        {
            if (_onCooldown) return;
            
            _status = GameStatus.Aim;
        }
        
        private void BlowUp()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_circle.transform.position, _radius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamageWithArmor(_damage, DamageType.Fire);
                }
            }
            _anim.BlowUp(transform.position, _radius);
        }
    }
}

