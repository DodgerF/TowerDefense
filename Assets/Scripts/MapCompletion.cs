using UnityEngine;
using SpaceShooter;
using System;


namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        [SerializeField] private EpisodeScore[] completionData;
        public bool TryIndex(int index, out Episode episode, out int score)
        {
            if (index >= 0 && index < completionData.Length)
            {
                episode = completionData[index].episode;
                score = completionData[index].score;
                return true;
            }
            episode = null;
            score = 0;
            return false;
        }
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var obj in completionData)
            { 
                if (obj.episode == currentEpisode)
                {
                    obj.score = obj.score < levelScore ? levelScore : obj.score;
                }
            }
        }
    }
}