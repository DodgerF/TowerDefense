using System.Runtime.CompilerServices;
using UnityEngine;

namespace TowerDefense
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

#if UNITY_EDITOR
        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }
#endif
    }
}
