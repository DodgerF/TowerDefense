using MyEventBus;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class UIGold : MonoBehaviour
    {
        private TextMeshProUGUI _uiText;

        #region Unity Events
        private void Awake()
        {
            _uiText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start()
        {
            
            EventBus.Instance.Subscribe<GoldHaveChangedSignal>(OnGoldChange);
        }

        private void OnDisable()
        {
            EventBus.Instance.Unsubscribe<GoldHaveChangedSignal>(OnGoldChange);
        }


        #endregion

        private void OnGoldChange(GoldHaveChangedSignal signal)
        {
            _uiText.text = signal.CurrentGold.ToString();
        }

    }
}