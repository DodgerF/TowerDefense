using SpaceShooter;
using UnityEngine;

namespace TowerDefense 
{
    public class EntitySpawner : Spawner
    {
        [SerializeField] private Entity[] _entityPrefabs;

        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(_entityPrefabs[Random.Range(0, _entityPrefabs.Length)].gameObject);
        }
    }
}
