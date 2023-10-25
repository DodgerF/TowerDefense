using MyEventBus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense {
    public class BuildPointController : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            EventBus.Instance.Invoke(new TowerBuyPointHaveClickedSignal(transform));
        }
    }
}
