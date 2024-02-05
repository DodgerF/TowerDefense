using TMPro;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel _rootLevel;
        [SerializeField] private int _needPoints;

        [SerializeField] private TextMeshProUGUI _amountStarText;

        private MapLevel _thisMapLevel;
        private void Awake()
        {
            _thisMapLevel = GetComponent<MapLevel>();
            _amountStarText.text = _needPoints.ToString();
            _thisMapLevel.SetActiveStarPanel(false);
        }
        public void TryActive()
        {
            gameObject.SetActive(_rootLevel.IsComplete);
           
            if (_needPoints <= MapCompletion.Instance.TotalScore )
            {
                _thisMapLevel.SetActiveStarPanel(true);
                _amountStarText.gameObject.transform.parent.gameObject.SetActive(false);
                _thisMapLevel.Initialise();
                
            }
        }
    }
}