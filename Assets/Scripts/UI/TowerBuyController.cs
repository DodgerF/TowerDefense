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

        [SerializeField] private EventBus _eventBus;
        #endregion

        #region Unity events
        private void Awake()
        {
            _uiText = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponentInChildren<Button>();
        }

        private void OnDisable()
        {
            if (_asset != null) _eventBus.Unsubscribe<GoldHaveChangedSignal>(OnGoldChange);
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
            if (_asset == null) return;

            _eventBus.Subscribe<GoldHaveChangedSignal>(OnGoldChange);

            _uiText.text = _asset.GoldCost.ToString();
            _button.image.sprite = _asset.TowerGUI;
        }

        public void Build()
        {
            _buildPoint.SetTower(_asset);
        }
        
    }
}
