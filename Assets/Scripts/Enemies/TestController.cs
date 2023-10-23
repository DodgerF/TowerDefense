using SpaceShooter;
using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Test))]
    public class TestController : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// ���� ��� �����.
        /// </summary>
        private Destructible _destructibleTerget;

        /// <summary>
        /// ���� ����.
        /// </summary>
        private Vector3 _moveTarget;

        /// <summary>
        /// ���� �� �������� ���� ��������.
        /// </summary>
        private Path _path;
        /// <summary>
        /// ������ ������� ����(_area), ������ �� ���� (_path).
        /// </summary>
        private int _pathIndex;

        /// <summary>
        /// ����� ����, � ������� ���� �������. �������� ������ ����.
        /// </summary>
        private Area _area;

        /// <summary>
        /// ������� ��������, ������ ����� ���������.
        /// </summary>
        private Test _character;

        #endregion

        #region Unity Events

        private void Awake()
        {
            _character = GetComponent<Test>();
        }
        private void Update()
        {
            UpdateCharacter();
        }

        #endregion

        #region Logic Methods

        /// <summary>
        /// ����� ���������.
        /// </summary>
        private void UpdateCharacter()
        {
            FindNewMoveTarget();
            MoveToTarget();
            FindNewAttackTarget();
            Attack();
            EvadeCollision();
        }

        private void EvadeCollision()
        {
            //��������
        }

        private void Attack()
        {
            //��������
        }

        private void FindNewAttackTarget()
        {
            //��������
        }

        private void MoveToTarget()
        {
            _character.Move(_moveTarget);
        }

        private void FindNewMoveTarget()
        {
            if (_destructibleTerget != null)
            {
                _moveTarget = _destructibleTerget.transform.position;
            }
            else
                if (_area != null)
            {
                bool isInsideArea = (_area.transform.position - transform.position).sqrMagnitude < _area.Radius * _area.Radius;

                if (isInsideArea)
                {
                    SetNextArea();
                }
                else
                {
                    // ���� �� �� � ����, �� ���� �� ���.
                    _moveTarget = _area.transform.position;
                }
            }
        }

        private void SetNextArea()
        {
            if (_path.Lenght > ++_pathIndex)
            {
                _area = _path[_pathIndex];
            }
            else
            {
                Destroy(gameObject);
            }

        }
        #endregion

        #region Public methods
        public void SetPath(Path path)
        {
            _path = path;
            _pathIndex = 0;
            _area = _path[_pathIndex];
        }
        #endregion
    }
}