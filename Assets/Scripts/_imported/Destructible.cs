using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// A destroyable object on the stage that can have hit points
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Destructible : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool indestructible;
        public bool IsIndestructible => indestructible;

        #region HP
        /// <summary>
        /// Initial hit points
        /// </summary>
        [Range(0, 100)]
        [SerializeField] protected int _maxHP;
        public int MaxHP => _maxHP;

        protected float _currentHP;
        public float GetCurrentHP => _currentHP;
        #endregion

        #endregion

        #region Unity Events
        protected virtual void OnEnable()
        {
            _currentHP = _maxHP;
        }

        #endregion

        #region Public API
        /// <summary>
        /// Applying damage to an object
        /// </summary>
        /// <param name="damage"> Damage dealt to object </param>
        public virtual void ApplyDamage(float damage)
        {

            if (indestructible) return;

            _currentHP -= damage;
           
            if (_currentHP <= 0)
            {
                OnDeath();
            }
        }

        public void SetHP(int value)
        {
            if (value <= 0) return;

            if(_maxHP < value)
            {
                _maxHP = value;
            }
            _currentHP = value;
        }

        #endregion

        /// <summary>
        /// Overridden object destruction event when hit points are less than 0
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }
        

        public const int TeamIdNeutral = 0;
        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;


        #region Score

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        #endregion

    }

}
