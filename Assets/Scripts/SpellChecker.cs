using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(Button))]
    public class SpellChecker : MonoBehaviour
    {
        [SerializeField] private Sprite _inactive;
        [SerializeField] private Spell _spell;
        private Button _button;
        private Image _img;
        private void Start()
        {
            _button = GetComponent<Button>();
            _img = GetComponent<Image>();

            if (_spell == null || Upgrades.GetUpgradeLevel(_spell.Asset) == 0)
            {
                _img.sprite = _inactive;
            }
            else
            {
                _img.sprite = _spell.Asset.Icon;
                _button.onClick.AddListener(_spell.Use);

                _spell.Cooldown += () =>
            {
                _img.sprite = _inactive;
            };
            _spell.CooledDown += () =>
            {
                _img.sprite = _spell.Asset.Icon;
            };
            }
            
        }
    }
}

