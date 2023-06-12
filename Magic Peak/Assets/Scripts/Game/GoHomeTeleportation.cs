using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHomeTeleportation : MonoBehaviour
{
    public string nextSceneName;
    public Vector3 playerPosition;
    private bool changingScene = false;
    [SerializeField] private WalletManager walletManager;

    void Awake()
    {
        walletManager = GameObject.Find("WalletManager").GetComponent<WalletManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        walletManager.WinCurrency();
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
