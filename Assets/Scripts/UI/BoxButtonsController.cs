using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TowerDefense 
{
    public class BoxButtonsController : MonoBehaviour
    {
        [SerializeField] private List<TowerAsset> _towers;
        [SerializeField] private TowerBuyController _prefabTBC;
        private List<TowerBuyController> _buttons;
        private const int MAX_NUMBER_OF_ASSETS = 4;

        private RectTransform _rectTransform;
        private Vector3 _startedPos;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startedPos = _rectTransform.anchoredPosition;
        }
        

        public void SetBuildPoint(BuildPointController point)
        {
            if (_buttons != null) foreach(var button in _buttons)
            {
                Destroy(button.gameObject);
            }
            _buttons = new List<TowerBuyController>();

            List<TowerAsset> buildableTowers;
            if (point.IsEmpty)
            {
                buildableTowers = GetBuildableTowers(_towers);
            }
            else
            {
                buildableTowers = GetBuildableTowers(point.GetTowers());
            }

            if (buildableTowers.Count == 0) return;

            for (int i = 0; i < buildableTowers.Count; i++)
            {
                if (i == MAX_NUMBER_OF_ASSETS) break;

                var button = Instantiate(_prefabTBC, transform);
                button.SetAsset(buildableTowers[i]);
                _buttons.Add(button);
                button.BuildPoint = point;
            }

            var angle = 90;

            for (int i = 0; i < _buttons.Count; i++)
            {
                var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 120);
                _buttons[i].transform.position += offset;
            }

            _rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(point.transform.position);
        }
        public void Hide()
        {
            _rectTransform.anchoredPosition = _startedPos;
        }

        private List<TowerAsset> GetBuildableTowers(List<TowerAsset> assets)
        {
            var buildableTowers = new List<TowerAsset>();
            if (assets == null) return buildableTowers;
            foreach(TowerAsset asset in assets)
            {
                if (buildableTowers.Count == MAX_NUMBER_OF_ASSETS) break;

                if (asset.IsAvailable)
                {
                    buildableTowers.Add(asset);
                }
            }

            return buildableTowers;
        }
    }
}