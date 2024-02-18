using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class SoundHook : MonoBehaviour
    {
        public Sound sound;
        public void Play() { sound.Play(); }
    }
}