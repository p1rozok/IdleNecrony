using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f; 
    private float currentHealth;
    public Text healthText; 

    public GameBalance gameBalance;
    public float baseReward = 50f; 

    private void Start()
    {
        if (gameBalance == null)
        {
            Debug.LogError("GameBalance не привязан в Enemy");
            return;
        }

        
        if (EnemySpawner.Instance != null)
        {
            int level = EnemySpawner.Instance.GetDefeatedEnemiesCount();
            Initialize(level);
            UpdateHealthUI();
        }
        else
        {
            Debug.LogError("EnemySpawner.Instance не найден!");
        }
    }

    public void Initialize(int level)
    {
       
        maxHealth = gameBalance.enemySettings.enemyBaseHealth * Mathf.Pow(gameBalance.enemySettings.enemyHealthMultiplier, level);
        baseReward = gameBalance.enemySettings.enemyBaseReward * Mathf.Pow(gameBalance.enemySettings.enemyRewardMultiplier, level);
        currentHealth = maxHealth;
        Debug.Log($"Враг инициализирован с уровнем: {level}, здоровьем: {maxHealth}, наградой: {baseReward}");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString("F0");
        }
    }

    private void Die()
    {
        Debug.Log("Враг уничтожен.");

      
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddCurrency(baseReward);
            Debug.Log($"Добавлено {baseReward} валюты.");
        }
        else
        {
            Debug.LogError("CurrencyManager.Instance не найден!");
        }

        
        if (EnemySpawner.Instance != null)
        {
            Debug.Log("Враг уничтожен. Передача данных в EnemySpawner: isCastle = false, cutsceneIndex = -1");
            EnemySpawner.Instance.EnemyDefeated(false, -1);
        }
        else
        {
            Debug.LogError("EnemySpawner.Instance не найден!");
        }

        Destroy(gameObject);
    }
}
