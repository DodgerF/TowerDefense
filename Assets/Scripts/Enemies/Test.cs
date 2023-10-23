using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Test : Destructible
    {
        #region Fields

        #region MoveSpeed
        [Range(0f, 100f)]
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed { get => _moveSpeed; set { if (value < 0) return; _moveSpeed = value; } }
        #endregion

        #endregion

        public void Move(Vector3 point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, _moveSpeed * Time.deltaTime);
        }
    }
}