using MyEventBus;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class UIGold : MonoBehaviour
    {
        private TextMeshProUGUI _uiText;

        #region Unity Events
        private void Start()
        {
            _uiText = GetComponentInChildren<TextMeshProUGUI>();

            _uiText.text = Player.Instance.Gold.ToString();

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