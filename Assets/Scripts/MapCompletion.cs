using UnityEngine;
using SpaceShooter;
using System;


namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string FILENAME = "completion.dat";
        private int _totalScore;
        public int TotalScore  => _totalScore;

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
            else
            {
                Debug.Log($"Episode complete with score: {levelScore}");
            }
        }
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var obj in _completionData)
            {
                if (obj.episode == currentEpisode)
                {
                    if (obj.score < levelScore)
                    {
                        obj.score = levelScore;
                        Saver<EpisodeScore[]>.Save(FILENAME, _completionData);
                    }
                }
            }
        }

        [SerializeField] private EpisodeScore[] _completionData;
        
        public void Load()
        {
            _totalScore = 0;
            foreach (var obj in _completionData)
            {
                obj.score = 0;
            }
            Saver<EpisodeScore[]>.TryLoad(FILENAME, ref _completionData); 
            foreach (var episodeScore in _completionData)
            {
                _totalScore += episodeScore.score;
            }
        }

        public int GetEpisodeScore(Episode episode)
        {
            foreach (var data in _completionData)
            {
                if (data.episode == episode)
                {
                    return data.score;
                }
            }
            return 0;
        }
    }
}