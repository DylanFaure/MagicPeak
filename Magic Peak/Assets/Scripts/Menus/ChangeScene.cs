using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToStart()
    {
        SceneManager.LoadScene("Start");
    }

    public void ChangeToSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
