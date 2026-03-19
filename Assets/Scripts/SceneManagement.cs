using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string SceneToLoad;

    public void swapScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void exitButton()
    {
        Application.Quit();
    }
}
