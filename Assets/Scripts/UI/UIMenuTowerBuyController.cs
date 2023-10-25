using MyEventBus;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense 
{
    public class UIMenuTowerBuyController : MonoBehaviour
    {
        [SerializeField] private TowerAsset[] _towers;
        private const int MAX_NUMBER_OF_ASSETS = 4;

        private List<UITowerBuyControl> _buttons = new List<UITowerBuyControl>();

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _buttons.AddRange(GetComponentsInChildren<UITowerBuyControl>());

            int index = 0;
            foreach (TowerAsset asset in _towers)
            {
                _buttons[index].SetAsset(asset);
                index++;

                if (index == MAX_NUMBER_OF_ASSETS) break;
            }

            foreach (UITowerBuyControl button in _buttons)
            {
                button.Init();
            }

            for (int i = 0; i < MAX_NUMBER_OF_ASSETS - index; i++)
            {
                _buttons.Remove(_buttons[MAX_NUMBER_OF_ASSETS - i - 1]);
            }


            EventBus.Instance.Subscribe<TowerBuyPointHaveClickedSignal>(SetNewPosition);
        }

        private void OnDisable()
        {
            EventBus.Instance.Subscribe<TowerBuyPointHaveClickedSignal>(SetNewPosition);
        }

        private void SetNewPosition(TowerBuyPointHaveClickedSignal signal)
        {
            var position = Camera.main.WorldToScreenPoint(signal.Transform.position);

            _rectTransform.anchoredPosition = position;

            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(true);
            }
        }
    }
}