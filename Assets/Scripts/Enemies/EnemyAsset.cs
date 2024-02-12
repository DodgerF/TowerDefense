using UnityEngine;


namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("View")]
        public Color Color = Color.white;
        public Vector2 SpriteScale = new Vector2 (5, 5);
        public RuntimeAnimatorController Animator;

        [Header("Game parameters")]
        public float MoveSpeed = 1;
        public int HP = 1;
        public int Armor = 0;
        public DamageType ProtectionFrom = DamageType.Physical;
        public int Score = 1;
        public int Gold = 1;
        public float Radius = 0.4f;
        public float Damage = 1;

        [Header("Enemy Type")]
        public Type Type = Type.Ground;
    }
}
