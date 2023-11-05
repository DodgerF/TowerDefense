using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuildPointController : MonoBehaviour
    {
        [SerializeField] private Tower _towerPrefab;

        [SerializeField] private SpriteRenderer _flagSprite;
        [SerializeField] private Image _button;
        private BoxButtonsController _buttonBox;

        private Tower _currentTower;

        private void Awake()
        {
            _buttonBox = FindAnyObjectByType<BoxButtonsController>();
        }

        public void TurnOnButtonBox()
        {
            _buttonBox.SetBuildPoint(this);
        }

        private void TurnOffButtonBox()
        {
            _buttonBox.Hide();
        }

        public void SetTower(TowerAsset asset)
        {
            if (!Player.Instance.Buy(asset.GoldCost)) return;

            _currentTower = Instantiate(_towerPrefab, transform.position, Quaternion.identity);
            _currentTower.UseAsset(asset);

            _button.gameObject.SetActive(false);
            _flagSprite.gameObject.SetActive(false);

            TurnOffButtonBox();
        }
    }
}