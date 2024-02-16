using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu()]
    public class Sounds : ScriptableObject
    {
        public AudioClip[] sounds;
        public AudioClip this[Sound s] => sounds[(int)s];
    }
}

