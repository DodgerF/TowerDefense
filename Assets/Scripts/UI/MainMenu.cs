using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        private void OnEnable()
        {
            _continueButton.interactable = FileHandler.HaveFile();
        }
        public void CreateNewGame()
        {
            FileHandler.Reset(MapCompletion.FILENAME);
            FileHandler.Reset(Upgrades.FILENAME);
            SceneManager.LoadScene(1);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
