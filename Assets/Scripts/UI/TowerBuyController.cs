using System;
using MyEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace TowerDefense
{
    public class TowerBuyController : MonoBehaviour
    {
        #region Fields
        private TowerAsset _asset;
        private TextMeshProUGUI _uiText;
        private Button _button;

        private BuildPointController _buildPoint;
        public BuildPointController BuildPoint { set { _buildPoint = value; } }

        private EventBus _eventBus;
        #endregion

        #region Unity events
        private void Awake()
        {
            _eventBus = FindObjectOfType<EventBus>();

            _uiText = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponentInChildren<Button>();
        }
        private void OnEnable()
        {
            _eventBus.Subscribe<GoldHaveChangedSignal>(OnGoldChange);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<GoldHaveChangedSignal>(OnGoldChange);
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

            _uiText.text = _asset.GoldCost.ToString();
            _button.image.sprite = _asset.TowerGUI;

            if (Player.Instance.Gold >= _asset.GoldCost != _button.interactable)
            {
                _button.interactable = !_button.interactable;
                _uiText.color = _button.interactable ? Color.white : Color.red;
            }
        }

        public void Build()
        {
            _buildPoint.SetTower(_asset);
        }
    }
}
