using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void LoadMainLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
