using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

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
        TogglePauseMenu();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
       Application.Quit();
    }
}
