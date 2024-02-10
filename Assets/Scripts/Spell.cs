using TowerDefense;
using UnityEngine;

namespace TowerDefense
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] protected UpgradeAsset _asset;
        [SerializeField] protected float _cooldown;
        protected float _time;
        protected bool _onCooldown;
        protected virtual void Update()
        {
            if (_onCooldown) 
            {
                _time += Time.deltaTime;
                if (_time >= _cooldown)
                {
                    _onCooldown = false;
                }
            }
        }

        public virtual void Use()
        {
            _onCooldown = true;
            _time = Time.time;
        }
    }
}
