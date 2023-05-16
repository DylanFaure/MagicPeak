using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    public string nextSceneName;
    public Vector3 playerPosition;
    private bool changingScene = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!changingScene && other.CompareTag("Player"))
        {
            changingScene = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = playerPosition;
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
        changingScene = false;
    }
}
