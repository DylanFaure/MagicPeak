using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneToCurrentScene()
    {
        if (PlayerPrefs.HasKey("CurrentScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("CurrentScene"));
        }
        else
        {
            SceneManager.LoadScene("Entrance");
        }
    }
}
