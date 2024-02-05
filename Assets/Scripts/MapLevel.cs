using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;


namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode _episode;

        [SerializeField] private Image[] _images;
        private int _score;
        public int Score => _score;

        public bool IsComplete { get {  return gameObject.activeSelf && _images[0].color == Color.white; } }
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

        public void SetActiveStarPanel(bool isActive)
        {
            _images[0].transform.parent.gameObject.SetActive(isActive);
        }

        public void Initialise()
        {
            _score = MapCompletion.Instance.GetEpisodeScore(_episode);

            int i = 0;
            while (i < _images.Length && _score > i)
            {
                _images[i++].color = Color.white;
            }
        }
    }
}