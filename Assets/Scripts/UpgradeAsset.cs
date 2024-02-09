using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite Icon;
        public int[] CostByLevel;
    }
}
