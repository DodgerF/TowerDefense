using SpaceShooter;
using UnityEditor;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : Destructible
    {
        #region Fields

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

        private void Awake()
        {
            _sprite = transform.Find("View").GetComponent<SpriteRenderer>();
            _animator = _sprite.GetComponent<Animator>();
            _circleCollider = GetComponentInChildren<CircleCollider2D>();
        }

        #endregion


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
            _circleCollider.radius = asset.radius;

            _moveSpeed = asset.moveSpeed;

            SetMaxHP(asset.hp);
        }
    }

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
}