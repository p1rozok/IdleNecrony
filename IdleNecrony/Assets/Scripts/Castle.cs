using UnityEngine;
using UnityEngine.UI;


public class Castle : MonoBehaviour
{
    public static event System.Action<int> OnCastleDestroyed; // ������� ��� ���������� �� ����������� �����

    public float maxHealth = 1000f;
    private float currentHealth;
    public Text healthText;

    public GameBalance gameBalance;
    public int cutsceneIndex;

    private void Start()
    {
        if (gameBalance == null)
        {
            Debug.LogError("GameBalance �� �������� � Castle");
            return;
        }

        currentHealth = maxHealth;
        UpdateHealthUI();
        Debug.Log($"����� ������ � ��������� ���������: {maxHealth}");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        Debug.Log($"����� ������� ����: {damage}. ������� ��������: {currentHealth}");
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
        Debug.Log("����� ��������!");

        OnCastleDestroyed?.Invoke(cutsceneIndex); // ����� ������� � ��������� ������� ��������

        if (EnemySpawner.Instance != null)
        {
            Debug.Log($"�������� ������ � EnemySpawner: isCastle = true, cutsceneIndex = {cutsceneIndex}");
            EnemySpawner.Instance.EnemyDefeated(true, cutsceneIndex);
        }
        else
        {
            Debug.LogError("EnemySpawner.Instance �� ������!");
        }

        Destroy(gameObject);
    }
}