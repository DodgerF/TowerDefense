using MyEventBus;
using UnityEngine;

namespace TowerDefense
{
    public class Player : SingletonBase<Player>
    {
        #region Fields

        #region HP
        [SerializeField] private int _maxHP;
        public int MaxHP => _maxHP;

        private float _currentHP;
        public float CurrentHP => _currentHP;
        #endregion

        #region Gold
        [SerializeField] private int _gold;
        public int Gold => _gold; 
        #endregion

        #endregion

        #region UnityEvents

        protected override void Awake()
        {
            base.Awake();

            _currentHP = _maxHP;
        }
        #region (Un)Subscribes

        private void OnEnable()
        {
            EventBus.Instance.Subscribe<PlayerIsAttackedSignal>(OnDamaged);
            EventBus.Instance.Subscribe<EnemyDiedSignal>(OnGotGold);
        }

        private void OnDisable()
        {
            EventBus.Instance.Unsubscribe<PlayerIsAttackedSignal>(OnDamaged);
            EventBus.Instance.Unsubscribe<EnemyDiedSignal>(OnGotGold);
        } 
        #endregion

        #endregion

        #region Private methods

        #region HP
        private void OnDamaged(PlayerIsAttackedSignal signal)
        {
            if (signal.Damage <= 0)
            {
                Debug.LogWarning("Damage must be possitive");
                return;
            }

            SetHP(_currentHP - signal.Damage);

            if (_currentHP <= 0)
            {
                EventBus.Instance.Invoke(new PlayerDiedSignal());
            }
        }

        private void SetHP(float hp)
        {
            _currentHP = hp;
            EventBus.Instance.Invoke(new HPHaveChangedSignal(_currentHP));
        }
        #endregion

        #region Gold
        private void OnGotGold(EnemyDiedSignal signal)
        {
            if (signal.Gold <= 0)
            {
                Debug.LogWarning("Gold must be possitive");
                return;
            }

            SetGold(_gold + signal.Gold);
        }

        private void SetGold(int gold)
        {
            _gold = gold;
            EventBus.Instance.Invoke(new GoldHaveChangedSignal(_gold));
        }  
        #endregion

        #endregion

        #region Score
        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }
        public void AddScore(int num)
        {
            if (num  < 0)
            {
                Debug.Log("�� ����� ���� ���������� ������������� �����.");
                return;
            }
            Score += num;
        }
        #endregion
    }
}