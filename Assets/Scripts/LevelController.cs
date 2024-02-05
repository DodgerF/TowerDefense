using MyEventBus;
using UnityEngine;

namespace TowerDefense
{
    public interface ILevelCondition
    {
        public bool IsComplited { get; }
    }
    public class LevelController : SingletonBase<LevelController>
    {
        #region Properties
        [SerializeField] private EventBus _eventBus;

        [SerializeField] private float m_ReferenceTime;
        public float ReferenceTime => m_ReferenceTime;
        [SerializeField] private int BonusScorePerSecond;

        private int _levelScore = 3;

        private ILevelCondition[] m_Conditions;
        private bool m_IsComplited;
        
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;
        #endregion

        #region UnityEvents
        private void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();

        }

        private void Update()
        {
            if (m_IsComplited) return;

            m_LevelTime += Time.deltaTime;

            CheckLevelConditions();
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<PlayerDiedSignal>(OnDied);
            _eventBus.Subscribe<HPHaveChangedSignal>(OnAttacked);
        }

       

        private void OnDisable()
        {
            _eventBus.Unsubscribe<PlayerDiedSignal>(OnDied);
            _eventBus.Unsubscribe<HPHaveChangedSignal>(OnAttacked);
        }

        #endregion

        #region Logic
        private bool _isSubtracted = false;
        private void OnAttacked(HPHaveChangedSignal signal)
        {
            if (_isSubtracted || signal.OldHP <= signal.CurrentHP) return;

            _levelScore--;
            _isSubtracted = true;
        }

        private void OnDied(PlayerDiedSignal signal)
        {
            StopLevel(false);
        }

        private void StopLevel(bool isSucces)
        {
            foreach (var animator in FindObjectsOfType<Animator>())
            {
                animator.speed = 0;
            }

            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
            DisableAll<Spawner>();
            DisableAll<Tower>();
            DisableAll<Projectile>();
            DisableAll<EnemyController>();

            ResultPanelController.Instance.ShowResults(isSucces);
        }

        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0) return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.IsComplited)
                {
                    numCompleted++;
                }
            }
            if (numCompleted == m_Conditions.Length)
            {
                m_IsComplited = true;

                if (m_LevelTime > m_ReferenceTime)
                {
                    _levelScore--;
                }
                StopLevel(true);
                MapCompletion.SaveEpisodeResult(_levelScore);
            }
        }
        #endregion
    }
}
