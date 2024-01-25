using UnityEngine;
using SpaceShooter;
using TMPro;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        private Episode _episode;

        [SerializeField] private TextMeshProUGUI _text;
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(_episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            _episode = episode;
            _text.text = $"{score}/3";
        }
    }
}