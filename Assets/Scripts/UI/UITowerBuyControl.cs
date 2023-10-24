using MyEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerBuyControl : MonoBehaviour
{
    [System.Serializable]
    public class TowerAsset
    {
        public int GoldCost;
        public Sprite TowerGUI;
    }

    [SerializeField] private TowerAsset _asset;
    private TextMeshProUGUI _uiText;
    private Button _button;

    private void Awake()
    {
        _uiText = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponentInChildren<Button>();
    }
    private void Start()
    {
        _uiText.text = _asset.GoldCost.ToString();
        _button.image.sprite = _asset.TowerGUI;

        EventBus.Instance.Subscribe<GoldHaveChangedSignal>(OnGoldChange);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GoldHaveChangedSignal>(OnGoldChange);
    }

    private void OnGoldChange(GoldHaveChangedSignal signal)
    {
        if (signal.CurrentGold >= _asset.GoldCost != _button.interactable)
        {
            _button.interactable = !_button.interactable;
            _uiText.color = _button.interactable ? Color.white : Color.red;
        }
    }
}
