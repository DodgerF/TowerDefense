using UnityEngine;
using System;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite Icon;
        public Info[] Inf;

        [Serializable]
        public class Info
        {
            public int GoldCost;
            public int SoulsCost;
            public float Damage;
            public float Cooldown;
            public int HP;
            public float Time;
            public float Percent;
            public float Radius;
        }
    }
}
