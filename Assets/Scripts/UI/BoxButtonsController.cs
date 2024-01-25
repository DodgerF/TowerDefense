using MyEventBus;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense 
{
    public class BoxButtonsController : MonoBehaviour
    {
        [SerializeField] private TowerAsset[] _towers;
        [SerializeField] private List<TowerBuyController> _buttons = new List<TowerBuyController>();

        private const int MAX_NUMBER_OF_ASSETS = 4;

        private RectTransform _rectTransform;
        private Vector3 _startedPos;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startedPos = _rectTransform.anchoredPosition;

            int index = 0;

            foreach (TowerAsset asset in _towers)
            {
                _buttons[index].SetAsset(asset);
                index++;

                if (index == MAX_NUMBER_OF_ASSETS) break;
            }
            
            for (int i = 0; i < MAX_NUMBER_OF_ASSETS - index; i++)
            {
                Destroy(_buttons[MAX_NUMBER_OF_ASSETS - i - 1].gameObject);
                _buttons.Remove(_buttons[MAX_NUMBER_OF_ASSETS - i - 1]);
            }

            
        }

        private void Start()
        {
            foreach (TowerBuyController button in _buttons)
            {
                button.Init();
            }
        }
        public void SetBuildPoint(BuildPointController point)
        {
            foreach (TowerBuyController button in _buttons)
            {
                button.BuildPoint = point;
            }
            SetNewPosition(point.transform.position);
        }
        public void Hide()
        {
            _rectTransform.anchoredPosition = _startedPos;
        }

        private void SetNewPosition(Vector3 pos)
        {

            var position = Camera.main.WorldToScreenPoint(pos);

            _rectTransform.anchoredPosition = position;
        }
    }
}