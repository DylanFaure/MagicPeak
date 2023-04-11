using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        bool isPaused = Time.timeScale == 0;
        pauseMenu.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1 : 0;
    }

    public void Resume()
    {
        print("Resume");
        TogglePauseMenu();
    }

    public void Restart()
    {
        print("Restart");
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        print("Start");
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        print("Quit");
       Application.Quit();
    }
}
