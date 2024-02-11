using MyEventBus;
using UnityEngine;

namespace TowerDefense
{
    public class Player : SingletonBase<Player>
    {
        #region Fields
        [SerializeField] private EventBus _eventBus;
        #region HP
        [SerializeField] private int _maxHP;
        public int MaxHP => _maxHP;

        private float _currentHP;
        public float CurrentHP => _currentHP;

        [SerializeField] private UpgradeAsset _healthUpgrade;
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

        private void Start()
        {
            _eventBus.Invoke(new GoldHaveChangedSignal(_gold));
            _eventBus.Invoke(new HPHaveChangedSignal(_currentHP, _currentHP));

            SetMaxHP(_maxHP + Upgrades.GetUpgradeLevel(_healthUpgrade) * 5);
            //TODO: change this ^ on "GetValueByLevel"
        }

        #region (Un)Subscribes
        private void OnEnable()
        {
            _eventBus.Subscribe<EnemyKilledSignal>(OnEnemyDied);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<EnemyKilledSignal>(OnEnemyDied);
        } 
        #endregion

        #endregion

        #region Methods

        #region HP
        public void Attacked(float damage)
        {
            if (damage <= 0)
            {
                Debug.LogWarning("Damage must be possitive");
                return;
            }

            SetHP(_currentHP - damage);
        }

        private void SetMaxHP(int hp) 
        {
            if (hp <= 0) 
            {
                Debug.LogWarning("Max HP must be positive number");
                return;
            }
            _maxHP = hp;
            SetHP(_maxHP);
        }
        private void SetHP(float hp)
        {
            if (hp > _maxHP)
            {
                hp = _maxHP;
            }
            float oldHP = _currentHP;
            _currentHP = hp;
            
            _eventBus.Invoke(new HPHaveChangedSignal(oldHP, _currentHP));

            if (_currentHP <= 0)
            {
                _eventBus.Invoke(new PlayerDiedSignal());
            }
        }
        #endregion

        #region Gold
        private void OnEnemyDied(EnemyKilledSignal signal)
        {
            if (signal.Gold <= 0)
            {
                Debug.LogWarning("Gold must be possitive");
                return;
            }

            SetGold(_gold + signal.Gold);
        }

        public void SetGold(int gold)
        {
            _gold = gold;
            _eventBus.Invoke(new GoldHaveChangedSignal(_gold));
        }

        public bool Buy(int cost)
        {
            if (cost > _gold) return false;

            SetGold(_gold - cost);

            return true;
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