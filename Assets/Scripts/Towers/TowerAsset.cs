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
        [SerializeField] private UpgradeAsset _requiredUpgrade;
        [SerializeField] private int _requiredUpgradeLevel;
        [SerializeField] private TowerAsset[] _nextAssets;
        public TowerAsset[] NextAssets => _nextAssets;
        public bool IsAvailable => !_requiredUpgrade || _requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(_requiredUpgrade);

        [Header("Тип войска для атаки")]
        public Type Type;
    }

}