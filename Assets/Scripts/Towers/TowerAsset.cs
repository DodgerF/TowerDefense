using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int GoldCost;
        public Sprite TowerGUI;
        public Sprite Sprite;

        public float Radius;
        public TurretProperties TurretProperties;
    }

}