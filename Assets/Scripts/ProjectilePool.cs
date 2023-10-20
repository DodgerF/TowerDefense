using MyObjectPool;
using UnityEngine;

namespace TowerDefense
{
    public class ProjectilePool : MyObjectPool<Projectile>
    {
        public ProjectilePool(Projectile prefab, int defaultCapacity
            ) : base(() => Preload(prefab), OnGet, OnReturn, defaultCapacity)
        {
        }

        public static Projectile Preload(Projectile prefab) => Object.Instantiate(prefab);
        public static void OnGet(Projectile obj) => obj.gameObject.SetActive(true);
        public static void OnReturn(Projectile obj) => obj.gameObject.SetActive(false);


    }
}
