using MyEventBus;
using SpaceShooter;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyController : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// ���� ��� �����.
        /// </summary>
        private Destructible _attackTarget;

        #region Path

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
        #endregion

        /// <summary>
        /// ������� ��������, ������� ������ ����� ���������.
        /// </summary>
        private Enemy _character;

        #endregion

        #region Unity Events

        private void Awake()
        {
            _character = GetComponent<Enemy>();
        }
        private void Update()
        {
            UpdateCharacter();
        }
        private void LateUpdate()
        {
            //������� �������. ���� ���� ����� - true, ����� right.
            bool isLeft = _moveTarget.x < _character.transform.position.x;
            _character.TurnCharacter(isLeft);
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
            if (_attackTarget != null)
            {
                _moveTarget = _attackTarget.transform.position;
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
                DealDamageToPlayer();
            }

        }

        private void DealDamageToPlayer()
        {
            _character.Attack();
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