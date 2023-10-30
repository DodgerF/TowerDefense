using TowerDefense;
using UnityEngine;

namespace SpaceShooter
{

    public class HomingMissile : Projectile
    {
     /*   [SerializeField] private float m_DistanceToEmptyTarget;
        [SerializeField] private float m_SearchRadius;

        private GameObject m_EmptyTarget;
        private Transform m_Target;

        protected override void Start()
        {
            base.Start();

            m_EmptyTarget = new GameObject("EmptyTarget");
            m_EmptyTarget.transform.position = _parent.transform.position + _parent.transform.up * m_DistanceToEmptyTarget;

            //Поиск ближайшего разрушаемого объекта в радиусе, который не является родителем снаряда 
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_parent.transform.position, m_SearchRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.transform.root.TryGetComponent<Destructible>(out Destructible target))
                {
                    if (target == _parent) continue;

                    if (m_Target == null)
                    {
                        m_Target = collider.transform;
                        continue;
                    }

                    if (Vector2.Distance(_parent.transform.position, m_Target.position) >
                        Vector2.Distance(_parent.transform.position, collider.transform.position))
                    {
                        m_Target = collider.transform;
                    }
                }
            }
        }

        protected override void Update()
        {
            if (m_Target == null)
            {
                base.Update();
                return;
            }

            float stepLenght = Time.deltaTime * _velocity;
            MoveObject(m_EmptyTarget.transform, Direction(m_EmptyTarget.transform, m_Target), stepLenght);
            
            Vector2 missilesDirection = Direction(transform, m_EmptyTarget.transform);

            CheckRaycastAhead(stepLenght, missilesDirection); 
            
            MoveObject(transform, missilesDirection, stepLenght);
            CheckTimer();

        }

        private Vector2 Direction(Transform obj, Transform target) 
        {
            Vector2 direction = target.position - obj.position;
            direction.Normalize();
            return direction;
        }

        private void MoveObject(Transform obj, Vector2 direction, float stepLenght)
        {
            Vector2 step = direction * stepLenght;
            obj.position += new Vector3(step.x, step.y, 0);
        }

        private void CheckRaycastAhead(float stepLenght, Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, stepLenght);
            if (hit)
            {
                if (hit.collider.transform.TryGetComponent<Destructible>(out Destructible destructible) && destructible != _parent)
                {
                    destructible.ApplyDamage(_damage);

                    UpdateScore(destructible);
                }
                OnProjectileLifeEnd();
            }
        }

        protected override void OnProjectileLifeEnd()
        {
            Destroy(m_EmptyTarget);
            base.OnProjectileLifeEnd();
        }*/
    }
}
