using MyEventBus;
using SpaceShooter;
using UnityEditor;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : Destructible
    {
        #region Fields

        private EventBus _events;

        #region Gold

        private int _gold;
        public int Gold => _gold;

        #endregion

        #region Damage
        private float _dmg;
        public float Damage => _dmg; 
        #endregion

        #region MoveSpeed
        [Range(0f, 1f)]
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed { get => _moveSpeed; set { if (value < 0) return; _moveSpeed = value; } }
        #endregion

        #region View
        private SpriteRenderer _sprite;
        private Animator _animator;
        private CircleCollider2D _circleCollider;
        #endregion

        #endregion

        #region Unity Events

        protected override void Awake()
        {
            base.Awake();

            _sprite = transform.Find("View").GetComponent<SpriteRenderer>();
            _animator = _sprite.GetComponent<Animator>();
            _circleCollider = GetComponentInChildren<CircleCollider2D>();
        }

        #endregion

        #region Override methods
        protected override void OnDeath()
        {
            EventBus.Instance.Invoke(new EnemyDiedSignal(_gold));

            base.OnDeath();
        }
        #endregion

        #region Public methods

        public void Move(Vector3 point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, _moveSpeed * Time.deltaTime);
        }

        public void TurnCharacter(bool isLeft)
        {
            _sprite.flipX = isLeft;
        }

        public void UseAsset(EnemyAsset asset)
        {
            _sprite.color = asset.Color;
            _sprite.transform.localScale = new Vector3(asset.SpriteScale.x, asset.SpriteScale.y, 1);
            _animator.runtimeAnimatorController = asset.Animator;
            _circleCollider.radius = asset.Radius;

            _moveSpeed = asset.MoveSpeed;

            _dmg = asset.Damage;

            _gold = asset.Gold;

            SetMaxHP(asset.HP);
        } 
        #endregion
    }

    #region Inspector
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset asset = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (asset != null)
            {
                (target as Enemy).UseAsset(asset);
            }
        }
    } 
    #endregion
}