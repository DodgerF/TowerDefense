using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        private MapLevel[] _levels;

        private void Awake()
        {
            _levels = GetComponentsInChildren<MapLevel>();
        }

        void Start ()
        {
            var drawLevel = 0;
            var score = 1;
            while (score != 0 && drawLevel < _levels.Length &&
                MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                _levels[drawLevel++].SetLevelData(episode, score);
            }


            for (int i = drawLevel; i < _levels.Length; i++)
            {
                _levels[i].gameObject.SetActive(false);
            }
        }
    }
}

