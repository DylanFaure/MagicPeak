using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public static WalletManager instance;
    private WalletData walletData;

    void Start()
    {
        LoadWalletData();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("Wallet instanced !");
        }
    }

    public void AddCurrency(int amount)
    {
        walletData.currencyAmount += amount;
        SaveWalletData();
    }

    public bool DeductCurrency(int amount)
    {
        if (walletData.currencyAmount >= amount)
        {
            walletData.currencyAmount -= amount;
            SaveWalletData();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetWalletData()
    {
        return walletData.currencyAmount;
    }

    private void SaveWalletData()
    {
        string json = JsonUtility.ToJson(walletData);
        PlayerPrefs.SetString("walletData", json);
    }

    private void LoadWalletData()
    {
        string json = PlayerPrefs.GetString("walletData");
        if (!string.IsNullOrEmpty(json)) {
            walletData = JsonUtility.FromJson<WalletData>(json);
        } else {
            walletData = new WalletData();
        }
    }
}
