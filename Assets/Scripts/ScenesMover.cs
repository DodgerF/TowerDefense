using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class ScenesMover : MonoBehaviour
    {
        public void ToMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void ToLevelMap()
        {
            SceneManager.LoadScene(1);
        }
        private string _shopScene = "ShopScene";
        public void ToShop()
        {
            SceneManager.LoadScene(_shopScene);
        }
    }
}