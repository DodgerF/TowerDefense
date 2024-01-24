using MyEventBus;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class UIGold : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus;
        
        private TextMeshProUGUI _uiText;

        #region Unity Events
        private void Awake()
        {
            _uiText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void OnEnable()
        {
            
            _eventBus.Subscribe<GoldHaveChangedSignal>(OnGoldChange);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<GoldHaveChangedSignal>(OnGoldChange);
        }


        #endregion

        private void OnGoldChange(GoldHaveChangedSignal signal)
        {
            _uiText.text = signal.CurrentGold.ToString();
        }

    }
}