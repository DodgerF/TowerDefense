using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class MissClick : MonoBehaviour, IPointerClickHandler
    {
        private BoxButtonsController _buttonBox;

        private void Awake()
        {
            _buttonBox = FindAnyObjectByType<BoxButtonsController>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _buttonBox.Hide();
        }

     
    }
}
