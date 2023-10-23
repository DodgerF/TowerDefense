using SpaceShooter;

using UnityEngine;

namespace TowerDefense
{
    public class EnemyController : AIController
    {
        #region Fields
        private Path m_Path;
        private int m_PathIndex;

        #endregion

        #region Overrided methods
        protected override void GetNewPoint()
        {
            if (m_Path.Lenght > ++m_PathIndex)
            {
                SetPatrolBehaviour(m_Path[m_PathIndex]);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion


        #region Private methods


        #endregion

        #region Public methods
        public void SetPath(Path path)
        {
            m_Path = path;
            m_PathIndex = 0;
            SetPatrolBehaviour(path[m_PathIndex]);
        }
        #endregion
    }
}