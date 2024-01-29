using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        private Episode _episode;

        [SerializeField] private Image[] _images;
        private void Awake()
        {
            foreach (Image image in _images)
            {
                image.color = Color.black;
            }
        }
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(_episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            _episode = episode;

            int i = 0;
            while (i < _images.Length && score > i)
            {
                _images[i++].color = Color.white;
            }
        }
    }
}