using SpaceShooter;
using UnityEditor;
using UnityEngine;

namespace TowerDefense {
    [RequireComponent (typeof(EnemyController))]
    public class Enemy : MonoBehaviour
    {
        public void Use(EnemyAsset asset)
        {
            var sprite = transform.Find("View").GetComponent<SpriteRenderer>();
            sprite.color = asset.Color;

            sprite.transform.localScale = new Vector3(asset.SpriteScale.x, asset.SpriteScale.y, 1);

            sprite.GetComponent<Animator>().runtimeAnimatorController = asset.Animator;


            GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            GetComponent<EnemyController>().NavigationLinear = asset.moveSpeed;

            SpaceShip enemy = GetComponent<SpaceShip>();
            enemy.SetMaxHP(asset.hp);
        }
    }

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI();
            EnemyAsset asset = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (asset != null)
            {
                (target as Enemy).Use(asset);
            }
        }
    }
}