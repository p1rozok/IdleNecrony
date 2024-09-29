using UnityEngine;
using UnityEngine.UI;


public class Castle : MonoBehaviour
{
    public static event System.Action<int> OnCastleDestroyed; // Событие для оповещения об уничтожении замка

    public float maxHealth = 1000f;
    private float currentHealth;
    public Text healthText;

    public GameBalance gameBalance;
    public int cutsceneIndex;

    private void Start()
    {
        if (gameBalance == null)
        {
            Debug.LogError("GameBalance не привязан в Castle");
            return;
        }

        currentHealth = maxHealth;
        UpdateHealthUI();
        Debug.Log($"Замок создан с начальным здоровьем: {maxHealth}");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        Debug.Log($"Замок получил урон: {damage}. Текущее здоровье: {currentHealth}");
        if (currentHealth <= 0)
        {
            DestroyCastle();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString("F0");
        }
    }

    private void DestroyCastle()
    {
        Debug.Log("Замок разрушен!");

        OnCastleDestroyed?.Invoke(cutsceneIndex); // Вызов события с передачей индекса катсцены

        if (EnemySpawner.Instance != null)
        {
            Debug.Log($"Передача данных в EnemySpawner: isCastle = true, cutsceneIndex = {cutsceneIndex}");
            EnemySpawner.Instance.EnemyDefeated(true, cutsceneIndex);
        }
        else
        {
            Debug.LogError("EnemySpawner.Instance не найден!");
        }

        Destroy(gameObject);
    }
}