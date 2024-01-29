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

        private void OnEnable()
        {
            foreach (MapLevel level in _levels)
            {
                level.gameObject.SetActive(false);
            }

            MapCompletion.Instance.Load();

            var drawLevel = 0;
            var score = 1;
            while (score != 0 && drawLevel < _levels.Length &&
                MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                _levels[drawLevel].gameObject.SetActive(true);
                _levels[drawLevel].SetLevelData(episode, score);
                drawLevel++;
            }
        }
    }
}

