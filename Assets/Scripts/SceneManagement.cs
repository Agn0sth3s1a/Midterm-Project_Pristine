using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string SceneToLoad; 
    
    [SerializeField] AudioClip ButtonSound;

    public void swapScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void exitButton()
    {
        Application.Quit();
    }

    public void buttonPressed()
    {
        SoundFXManager.Instance.PlaySoundFXClip(ButtonSound, transform, 1f);
    }
}
