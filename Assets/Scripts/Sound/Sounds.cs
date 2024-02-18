using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [CreateAssetMenu()]
    public class Sounds : ScriptableObject
    {
        [SerializeField] private AudioClip[] _sounds;
        public AudioClip this[Sound s] => _sounds[(int)s];

        #if UNITY_EDITOR
        [CustomEditor(typeof(Sounds))]
        public class SoundsInspector : Editor
        {
            private static readonly int soundCount = Enum.GetValues(typeof(Sound)).Length;
            private new Sounds target => base.target as Sounds;
            public override void OnInspectorGUI()
            {
                if (target._sounds.Length < soundCount)
                {
                    Array.Resize(ref target._sounds, soundCount);
                }
                for (int i = 0; i < target._sounds.Length; i++)
                {
                    target._sounds[i] = EditorGUILayout.ObjectField($"{(Sound)i}:", target._sounds[i], typeof(AudioClip), false) as AudioClip;
                }
            }
        }
        #endif
    }
}

