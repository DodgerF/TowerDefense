using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea _startArea;
        public CircleArea StartArea { get { return _startArea; } }

        [SerializeField] private Area[] _points;
        public int Lenght => _points.Length;
        public Area this[int i] => _points[i];

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach(var p in _points)
            {
                if (p != null)
                    Gizmos.DrawSphere(p.transform.position, p.Radius);
            }
        }
#endif
    }
}