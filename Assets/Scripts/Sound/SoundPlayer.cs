using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private Sounds _sounds;
        [SerializeField] private AudioClip _backgroundMusic;
        private AudioSource _audio;
        private new void Awake()
        {
            _audio = GetComponent<AudioSource>(); 
            base.Awake();

            if (_backgroundMusic != _audio.clip)
            {
                Instance._audio.clip = _backgroundMusic;
                Instance._audio.Play();
            }
        }
        public void Play(Sound sound)
        {
            _audio.PlayOneShot(_sounds[sound]);
        }
    }
}

