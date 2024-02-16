using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(Button))]
    public class SoundHook : MonoBehaviour
    {
        public Sound sound;
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(Play);
        }
        public void Play() { sound.Play(); }
    }
}