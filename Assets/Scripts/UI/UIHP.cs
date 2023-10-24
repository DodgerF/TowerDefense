using MyEventBus;
using TMPro;
using UnityEngine;

namespace TowerDefense {
    public class UIHP : MonoBehaviour
    {
        private TextMeshProUGUI _uiText;

        #region Unity Events
        private void Start()
        {
            _uiText = GetComponentInChildren<TextMeshProUGUI>();

            _uiText.text = Player.Instance.MaxHP.ToString();

            EventBus.Instance.Subscribe<HPHaveChangedSignal>(OnHPChanged);
        }

        private void OnDisable()
        {
            EventBus.Instance.Unsubscribe<HPHaveChangedSignal>(OnHPChanged);
        }
        #endregion

        private void OnHPChanged(HPHaveChangedSignal signal)
        {
            _uiText.text = signal.CurrentHP.ToString();
        }

    }
}
