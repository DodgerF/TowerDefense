using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense {
    public class BuildPointButton : MonoBehaviour, IPointerClickHandler
    {
        private BuildPointController controller;
        private void Awake()
        {
            controller = transform.root.GetComponent<BuildPointController>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            controller.TurnOnButtonBox();
        }
    }
}
