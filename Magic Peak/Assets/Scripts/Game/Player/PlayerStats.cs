using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 0;

    [Header("Stats")]
    [SerializeField] private float attackDamage = 15f;
    [SerializeField] private float level = 1;
    [SerializeField] private float experience = 0;
    [SerializeField] private float experienceNeeded = 100;

    [Header("Settings")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private string changeScene;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI levelText;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GetSavedUserStat();
    }

    void Update()
    {
        DestroyPlayer();
        DisplayHealth();
        DisplayLevel();
        if (experience >= experienceNeeded)
        {
            level++;
            experience -= experienceNeeded;
            experienceNeeded = Mathf.RoundToInt(experienceNeeded * 1.5f);
            maxHealth += 10;
            HealPlayer(maxHealth);
            attackDamage += 5;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void HealPlayer(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void GainXp(float gainedXp)
    {
        experience += gainedXp;
    }

    public float GetAttackPlayer()
    {
        return attackDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            TakeDamage(20);
        }
    }

    // Save user data (health and level) after a map
    private void SaveUserStat()
    {
        PlayerPrefs.SetFloat("currentHealth", currentHealth);
        PlayerPrefs.SetFloat("level", level);
    }

    private void GetSavedUserStat()
    {
        currentHealth = PlayerPrefs.GetFloat("currentHealth");
        level = PlayerPrefs.GetFloat("level");
    }

    private void DisplayHealth()
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }

    private void DisplayLevel()
    {
        levelText.text = level.ToString();
    }

    private void DestroyPlayer()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(changeScene);
        }
    }
}
