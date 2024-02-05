using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense
{
    public class MyObjectPool : MonoBehaviour
    {
        public enum PoolType
        {
            Projectiles,
            Enemies,
            None
        }
        private class PooledObjectInfo
        {
            public string LookupString;
            public List<GameObject> InactiveObjects = new List<GameObject>();
        }
        private class Holder
        {
            private static GameObject ObjectPoolHolder = new GameObject("Pooled Objects");
            private static List<Holder> _holders = new List<Holder>();
            public static IReadOnlyList<Holder> Holders => _holders;

            private GameObject _holderObj;
            public GameObject Obj => _holderObj;
            private PoolType _type;
            public PoolType Type { get { return _type; } }
            public Holder(PoolType type)
            {
                _holderObj = new GameObject(type.ToString());
                _holderObj.transform.SetParent(ObjectPoolHolder.transform);
                _type = type;
                _holders.Add(this);
            }
        }

        private static List<PooledObjectInfo> _objectPools = new List<PooledObjectInfo>();
        

        private void Awake()
        {
            SetupHolders();
        }
        private static Holder _arrowsHolder;
        private static Holder _enemiesHolder;
        private void SetupHolders()
        {
            _arrowsHolder = new Holder(PoolType.Projectiles);
            _enemiesHolder = new Holder(PoolType.Enemies);
        }

        public static bool isEnemies()
        {
            var transform = _enemiesHolder.Obj.transform;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }

        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPoint, Quaternion spawnRotation, PoolType poolType = PoolType.None)
        {
            PooledObjectInfo pool = _objectPools.Find(p => p.LookupString == objectToSpawn.name);

            if (pool == null)
            {
                pool = new PooledObjectInfo() { LookupString = objectToSpawn.name};
                _objectPools.Add(pool);
            }

            GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

            if (spawnableObj == null)
            {
                GameObject parent = GetParentObject(poolType);
                spawnableObj = Instantiate(objectToSpawn, spawnPoint, spawnRotation);
                if (parent != null)
                {
                    spawnableObj.transform.SetParent(parent.transform);
                }
            }
            else
            {
                spawnableObj.transform.position = spawnPoint;
                spawnableObj.transform.rotation = spawnRotation;
                pool.InactiveObjects.Remove(spawnableObj);
                spawnableObj.SetActive(true);
            }

            return spawnableObj;
        }
        public static void ReturnObjectToPool(GameObject obj)
        {
            string goName = obj.name.Substring(0, obj.name.Length - 7);

            PooledObjectInfo pool = _objectPools.Find(p => p.LookupString == goName);

            if (pool == null)
            {
                Debug.LogWarning("Trying to realease an object that is not pooled: " + obj.name);
            }

            else
            {
                obj.SetActive(false);
                pool.InactiveObjects.Add(obj);
            }
        }

        private static GameObject GetParentObject(PoolType poolType)
        {
            foreach(Holder holder in Holder.Holders)
            {
                if (holder.Type == poolType) return holder.Obj;
            }
            return null;
        }
    }

    
}