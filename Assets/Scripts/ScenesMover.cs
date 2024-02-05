using UnityEngine;
using UnityEngine.SceneManagement;

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
}
