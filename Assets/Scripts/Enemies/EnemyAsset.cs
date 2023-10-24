using UnityEngine;


namespace TowerDefense
{


    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        public Color Color = Color.white;
        public Vector2 SpriteScale = new Vector2 (5, 5);
        public RuntimeAnimatorController Animator;

        [Header("Параметры")]
        public float MoveSpeed = 1;
        public int HP = 1;
        public int Score = 1;
        public int Gold = 1;
        public float Radius = 0.4f;
        public float Damage = 1;

    }
}
