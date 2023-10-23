using UnityEngine;

namespace TowerDefense
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Path _path;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private EnemyAsset[] _enemyAssets;

        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(_enemyPrefab);
            e.Use(_enemyAssets[Random.Range(0, _enemyAssets.Length)]);
            e.GetComponent<EnemyController>().SetPath(_path);
            return e.gameObject;
        }
    }
}