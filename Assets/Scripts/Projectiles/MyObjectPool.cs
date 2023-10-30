using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense
{
    public class MyObjectPool : MonoBehaviour
    {
        public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

        private GameObject _objectPoolHolder;

        private static GameObject _arrowsHolder;
        private static GameObject _enemiesHolder;

        public enum PoolType
        {
            Arrows,
            Enemies,
            None
        }
        public static PoolType PoolingType;

        private void Awake()
        {
            SetupHolders();
        }
        private void SetupHolders()
        {
            _objectPoolHolder = new GameObject("Pooled Objects");

            _arrowsHolder = new GameObject("Arrows");
            _arrowsHolder.transform.SetParent(_objectPoolHolder.transform);

            _enemiesHolder = new GameObject("Enemies");
            _enemiesHolder.transform.SetParent(_objectPoolHolder.transform);
        }

        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPoint, Quaternion spawnRotation, PoolType poolType = PoolType.None)
        {
            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

            if (pool == null)
            {
                pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
                ObjectPools.Add(pool);
            }

            GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

            if (spawnableObj == null)
            {
                GameObject parentObject = SetParentObject(poolType);
                spawnableObj = Instantiate(objectToSpawn, spawnPoint, spawnRotation);
                if (parentObject != null)
                {
                    spawnableObj.transform.SetParent(parentObject.transform);
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

            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

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

        private static GameObject SetParentObject(PoolType poolType)
        {
            switch(poolType)
            {
                case PoolType.None:
                    return null;
                case PoolType.Arrows:
                    return _arrowsHolder;
                case PoolType.Enemies:
                    return _enemiesHolder;
                default: 
                    return null;
            }
        }
    }

    public class PooledObjectInfo
    {
        public string LookupString;
        public List<GameObject> InactiveObjects = new List<GameObject>();
    }
}