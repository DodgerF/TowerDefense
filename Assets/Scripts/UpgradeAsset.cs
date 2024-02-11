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
            public int Cost;
            public float Damage;
            public float Cooldown;
            public int HP;
            public float Time;
        }
    }
}
