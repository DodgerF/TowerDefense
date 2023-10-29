using TowerDefense;
using UnityEngine;

namespace MyEventBus
{
    public class TowerBuyPointHaveClickedSignal
    {
        private BuildPointController _point;
        public BuildPointController Point => _point;
        public TowerBuyPointHaveClickedSignal(BuildPointController point)
        {
            _point = point;
        }
    }
}