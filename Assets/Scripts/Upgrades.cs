using System;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string FILENAME = "upgrades.dat";
        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset Asset;
            public int Level = 0;
        }
        [SerializeField] private UpgradeSave[] _saves;
        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(FILENAME, ref _saves);
        }
        public static int LevelUpUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance._saves)
            {
                if (upgrade.Asset == asset)
                {
                    upgrade.Level++;
                    Saver<UpgradeSave[]>.Save(FILENAME, Instance._saves);
                    return upgrade.Level;
                }
            }
            return 0;
        }
        //TODO: add "GetValueByLevel"
        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            if (!Instance)
            {
                Debug.LogWarning("Upgrades.Instance doesn't exist!");
                return 0;
            }

            foreach (var upgrade in Instance._saves)
            {
                if (upgrade.Asset == asset)
                {
                    return upgrade.Level;
                }
            }
            return 0;
        }
        public static int GetSpentScore()
        {
            var result = 0;
            foreach (var upgrade in Instance._saves)
            {
                for (int i = 0; i < upgrade.Level; i++) 
                {
                    result += upgrade.Asset.Inf[i].Cost;
                }
            }
            return result;
        }

    }
}