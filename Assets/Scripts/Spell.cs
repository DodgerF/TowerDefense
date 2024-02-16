using System;
using UnityEngine;

namespace TowerDefense
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] protected UpgradeAsset _asset;
        public UpgradeAsset Asset => _asset;
        public int GetSoulsCost => _asset.Inf[GetLevel()].SoulsCost;
        protected float _cooldown;
        protected float _time;
        protected bool _onCooldown;
        public event Action CooledDown;
        public event Action Cooldown;
        protected virtual void Update()
        {
            if (_onCooldown) 
            {
                _time += Time.deltaTime;
                if (_time >= _cooldown)
                {
                    _onCooldown = false;
                    CooledDown?.Invoke();
                }
            }
        }
        public virtual void Use()
        {
            _onCooldown = true;
            _time = Time.time;
            
            
            _cooldown = _asset.Inf[GetLevel()].Cooldown + Time.time;
            Cooldown?.Invoke();
        }
        protected int GetLevel()
        {
            var level = Upgrades.GetUpgradeLevel(_asset);
            print($"{_asset.name} {Upgrades.GetUpgradeLevel(_asset)}");
            if (level != 0)
            {
                level--;
            }
            return level;
        }
    }
}
