using UnityEngine;
using System.Collections.Generic;
using System;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite Icon;
        public CostAndValue[] Info;
        
        [Serializable]
        public class CostAndValue
        {
            public int Cost;
            public float Value;
        }
    }
}
