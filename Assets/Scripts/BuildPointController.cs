using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuildPointController : MonoBehaviour
    {
        [SerializeField] private Tower _towerPrefab;
        [SerializeField] private SpriteRenderer _flagSprite;
        private TowerAsset[] _upgrades;
        private BoxButtonsController _buttonBox;

        private Tower _currentTower;
        public bool IsEmpty => _currentTower == null;

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
        public List<TowerAsset> GetTowers()
        {
            if (_upgrades == null) return null;

            return _upgrades.ToList();
        }
        public void SetTower(TowerAsset asset)
        {
            if (!Player.Instance.Buy(asset.GoldCost)) return;

            if (_currentTower != null)
            {
                Destroy(_currentTower.gameObject);
            }

            _currentTower = Instantiate(_towerPrefab, transform.position, Quaternion.identity);
            _currentTower.UseAsset(asset);

            _upgrades = asset.NextAssets;
            _flagSprite.gameObject.SetActive(false);

            TurnOffButtonBox();
        }
    }
}