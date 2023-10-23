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
        public float moveSpeed = 1;
        public int hp = 1;
        public int score = 1;
        public float radius = 0.4f;

    }
}
