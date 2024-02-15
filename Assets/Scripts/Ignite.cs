using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{

    [RequireComponent(typeof(Explosion))]
    public class Ignite : Spell
    {
        private GameObject _circle;
        private Vector3 _startPos;
        private Explosion _anim;
        private float _damage;
        private float _radius;

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(_asset);

            _anim = GetComponent<Explosion>();
            _damage = _asset.Inf[level].Damage;
            _radius = _asset.Inf[level].Radius;

            _circle = GetComponentInChildren<SpriteRenderer>().gameObject;
            _startPos = _circle.transform.position;
        }

        protected override void Update()
        {
            if (StatusMachine.currentStatus == GameStatus.Aim)
            {
                var mousePos = Input.mousePosition;
                mousePos.z = 10;
                _circle.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

                if (Input.GetMouseButtonDown(0))
                {
                    BlowUp(); 
                    base.Use();

                    Off();                    
                }
                if (Input.GetKey(KeyCode.Escape))
                {
                    Off();
                }
            }
            base.Update();
        }

        public override void Use()
        {
            if (_onCooldown) return;
            StatusMachine.currentStatus = GameStatus.Aim;
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
            _anim.BlowUp(_circle.transform.position, _radius);
        }

        private void Off()
        {
            _circle.transform.position = _startPos;
            StatusMachine.currentStatus = GameStatus.Base;
        }
    }
}

