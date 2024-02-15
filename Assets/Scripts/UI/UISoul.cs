using MyEventBus;
using TMPro;
using UnityEngine;

namespace TowerDefense {
    public class UISoul : MonoBehaviour
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
           _eventBus.Subscribe<SoulsHaveChangedSignal>(SetText);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<SoulsHaveChangedSignal>(SetText);
        }
        #endregion

        private void SetText(SoulsHaveChangedSignal signal)
        {
            _uiText.text = signal.CurrentSouls.ToString();
        }

    }
}
