using MyEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UITowerBuyControl : MonoBehaviour
    {
        #region Fields

        private TowerAsset _asset;
        private TextMeshProUGUI _uiText;
        private Button _button;

        #endregion

        #region Unity events
        private void Awake()
        {
            _uiText = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponentInChildren<Button>();
        }

        private void OnDisable()
        {
            if (EventBus.Instance == null) return;

            EventBus.Instance.Unsubscribe<GoldHaveChangedSignal>(OnGoldChange);
        }

        #endregion

        private void OnGoldChange(GoldHaveChangedSignal signal)
        {
            if (signal.CurrentGold >= _asset.GoldCost != _button.interactable)
            {
                _button.interactable = !_button.interactable;
                _uiText.color = _button.interactable ? Color.white : Color.red;
            }
        }
        public void SetAsset(TowerAsset asset)
        {
            _asset = asset;
        }

        public void Init()
        {
            EventBus.Instance.Subscribe<GoldHaveChangedSignal>(OnGoldChange);

            if (_asset == null)
            {
                Destroy(gameObject);
                return;
            }
            _uiText.text = _asset.GoldCost.ToString();
            _button.image.sprite = _asset.TowerGUI;
        }
    }
}
