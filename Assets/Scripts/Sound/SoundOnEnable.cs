using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(SoundHook))]
    public class SoundOnEnable : MonoBehaviour
    {
        private SoundHook sound;
        private void Awake()
        {
            sound = GetComponent<SoundHook>();
        }
        private void OnEnable()
        {
            sound.Play();
        }
    }
}

