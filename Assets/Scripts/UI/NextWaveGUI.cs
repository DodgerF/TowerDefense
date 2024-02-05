using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _bonusAmount;
        private EnemyWaveManager _waveManager;
        private float _timeToNextWave = 0f;
        
        private void Awake()
        {
            _waveManager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                _timeToNextWave = time;
            };
            EnemyWave.OnWaveDisable += () =>
            {
                _timeToNextWave = 0f;
            };
        }
        private void Update()
        {
            var bonus = (int)_timeToNextWave;
            if (bonus < 0) bonus = 0;
            _bonusAmount.text = bonus.ToString();
            _timeToNextWave -= Time.deltaTime;
        }

        public void CallWave()
        {
            _waveManager.ForceNextWave();
        }
    }
}