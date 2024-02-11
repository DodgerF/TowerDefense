using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        private List<MapLevel> _levels = new List<MapLevel>();
        private List<BranchLevel> _branches = new List<BranchLevel>();

        private void Awake()
        {
            foreach (MapLevel level in GetComponentsInChildren<MapLevel>())
            {
                if (level.TryGetComponent<BranchLevel>(out var branch))
                {
                    _branches.Add(branch);
                }
                else
                {
                    _levels.Add(level);
                }
            }
        }
        
        private void Start()
        {
            SetDisable(_levels);
            SetDisable(_branches);

            MapCompletion.Instance.Load();

            var drawLevel = 0;

            do
            {
                _levels[drawLevel].Initialise();
                _levels[drawLevel].gameObject.SetActive(true);

            } while (_levels[drawLevel].Score > 0 && ++drawLevel < _levels.Count);


            foreach (BranchLevel branch in _branches)
            {
                branch.TryActive();
            }


        }
        private void SetDisable<T>(List<T> objects) where T : MonoBehaviour
        {
            foreach(T obj in objects)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}

