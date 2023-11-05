using MyEventBus;
using TMPro;
using UnityEngine;

namespace TowerDefense {
    public class UIHP : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus;
        private TextMeshProUGUI _uiText;

        #region Unity Events
        private void Awake()
        {
            _uiText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void OnEnable()
        {
           _eventBus.Subscribe<HPHaveChangedSignal>(OnHPChanged);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<HPHaveChangedSignal>(OnHPChanged);
        }
        #endregion

        private void OnHPChanged(HPHaveChangedSignal signal)
        {
            _uiText.text = signal.CurrentHP.ToString();
        }

    }
}
