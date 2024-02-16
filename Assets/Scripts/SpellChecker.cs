using MyEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(Button))]
    public class SpellChecker : MonoBehaviour
    {
        [SerializeField] private Sprite _inactive;
        [SerializeField] private Spell _spell;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _panel;
        private int _cost;
        private Button _button;
        private void Awake()
        {
            _button = GetComponent<Button>();

            _panel.SetActive(false);
        }
        private void Start()
        {
            _cost = _spell.GetSoulsCost;
            _panel.GetComponentInChildren<TextMeshProUGUI>().text = $"X{_cost}";
            if (_spell == null || Upgrades.GetUpgradeLevel(_spell.Asset) == 0)
            {
                _icon.sprite = _inactive;
            }
            else
            {
                _icon.sprite = _spell.Asset.Icon;
                _button.onClick.AddListener(_spell.Use);

                _button.onClick.AddListener(() => {
                    Player.Instance.BuyForSouls(_cost);
                });

                _spell.Cooldown += () =>
                {
                    _icon.sprite = _inactive;
                };
                _spell.CooledDown += () =>
                {
                    _icon.sprite = _spell.Asset.Icon;
                };
            }
        }
        private void Update() 
        {
            if (_icon.sprite == _inactive)
            {
                _panel.SetActive(false);
                _button.interactable = false;
            }
            else
            {
                if (_cost > Player.Instance.Souls)
                {
                    _panel.SetActive(true);
                    _button.interactable = false;
                }
                else
                {
                    _panel.SetActive(false);
                    _button.interactable = true;
                }
            }
        }
       
    }
}

