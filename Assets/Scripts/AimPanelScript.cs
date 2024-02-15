using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class AimPanelScript : MonoBehaviour
    {
        private GameObject _panel;

        private void Awake()
        {
            _panel = GetComponentInChildren<Image>().gameObject;
            _panel.SetActive(false);
        }
        private void Update()
        {
            if (StatusMachine.currentStatus == GameStatus.Aim)
            {
                _panel.SetActive(true);
            }
            else
            {
                _panel.SetActive(false);
            }
        }
    }

}
