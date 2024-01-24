using MyEventBus;
using SpaceShooter;
using System;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        #region Properties
        [SerializeField] private TextMeshProUGUI m_ResultText;


        [SerializeField] private TextMeshProUGUI m_NumKillsText;
        [SerializeField] private TextMeshProUGUI m_ScoreText;
        [SerializeField] private TextMeshProUGUI m_TimeText;

        [SerializeField] private TextMeshProUGUI m_ButtonText;

        private bool m_Success;



        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;

        #endregion

        #region Unity Events
        protected override void Awake()
        {
            base.Awake();

            _losePanel.SetActive(false);
            _winPanel.SetActive(false);
        }

        #endregion

        #region API
        public void ShowResults(bool success)
        {
            if (success)
            {
                _winPanel.SetActive(true);
            }
            else
            {
                _losePanel.SetActive(true);
            }
        }

        public void OnButtonNext()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;

            if (m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                gameObject.SetActive(false);
                LevelSequenceController.Instance.RestartLevel();
            }

        }

        #endregion


    }
}