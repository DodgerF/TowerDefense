using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense 
{
    public class ShopCell : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _upgradeLevel, _cost;
        [SerializeField] private Button _button;
        private Shop _shop;
        private UpgradeAsset _asset;
        private int _savedLevel;

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
        public void CheckCost()
        {
            if (_savedLevel <= _asset.Inf.Length && _asset.Inf[_savedLevel - 1].GoldCost > _shop.PlayerMoney)
            {
                _button.interactable = false;
            }
        }
        public void Initialize(UpgradeAsset asset)
        {
            _shop =  GetComponentInParent<Shop>();
            _asset = asset;
            _savedLevel = Upgrades.GetUpgradeLevel(asset) + 1;

            _button.onClick.AddListener(Buy);
            
            UpdateView();
            CheckCost();
        }

        private void Buy()
        {
            _savedLevel = Upgrades.LevelUpUpgrade(_asset) + 1;
            _shop.UpdateMoney();
            UpdateView();
            
        }
        private void UpdateView()
        {
            _icon.sprite = _asset.Icon;
            if (_savedLevel > _asset.Inf.Length)
            {
                _button.interactable = false;
                foreach (RectTransform obj in _button.GetComponentInChildren<RectTransform>())
                {
                    obj.gameObject.SetActive(false);
                }
                _cost.text = "";
                _upgradeLevel.text = "MAX";
            }
            else
            {
                _upgradeLevel.text = $"Level {_savedLevel}";
                _cost.text = _asset.Inf[_savedLevel - 1].GoldCost.ToString();
                
            }
           
        }
        
    }
}