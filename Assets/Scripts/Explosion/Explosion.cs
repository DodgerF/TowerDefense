using UnityEngine;

namespace TowerDefense
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _explosionTime;
        [SerializeField] private GameObject _prefab;

        public void BlowUp(Vector3 pos, float scale = 1)
        {
            GameObject explosion = Instantiate(_prefab, pos, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * scale;
        }
    }
}

