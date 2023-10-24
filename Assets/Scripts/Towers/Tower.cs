using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        private Turret[] m_Turrets;
        private Destructible m_Target;

        #region Unity Events

        private void Awake()
        {
            m_Turrets = GetComponentsInChildren<Turret>();
            m_Target = null;
        }

        private void Update()
        {
            if (m_Target)
            {
                CircleCollider2D collider = m_Target.GetComponentInChildren<CircleCollider2D>();
                Vector2 targetVector = collider.transform.position - transform.position;

                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (Turret turret in m_Turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }
                else
                {
                    m_Target = null;
                }
                
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    m_Target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
#endif
    }
}
