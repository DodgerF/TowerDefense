using UnityEngine;

namespace MyEventBus
{
    public class TowerBuyPointHaveClickedSignal
    {
        private Transform _transform;
        public Transform Transform => _transform;
        public TowerBuyPointHaveClickedSignal(Transform transform)
        {
            _transform = transform;
        }
    }
}