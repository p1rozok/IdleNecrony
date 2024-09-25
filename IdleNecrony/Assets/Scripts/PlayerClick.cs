
using UnityEngine;
using UnityEngine.UI;

public class PlayerClick : MonoBehaviour
{
    public float clickDamage = 1f;
    public float clickUpgradeCost = 100f;

    public Text clickDamageText;
    public Text clickUpgradeCostText;
    public Button clickUpgradeButton;

    public GameBalance gameBalance; 

    private void Start()
    {
        if (gameBalance == null)
        {
            Debug.LogError("GameBalance �� �������� � PlayerClick");
            return;
        }

        UpdateClickStatsUI();

        if (clickUpgradeButton != null)
        {
            clickUpgradeButton.onClick.AddListener(UpgradeClickDamage);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(worldPoint);

            if (collider != null)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(clickDamage);
                }
                else
                {
                    Castle castle = collider.GetComponent<Castle>();
                    if (castle != null)
                    {
                        castle.TakeDamage(clickDamage);
                    }
                }

                
                if (CurrencyManager.Instance != null)
                {
                    CurrencyManager.Instance.AddCurrency(gameBalance.currencySettings.currencyGenerationRate * clickDamage);
                }
            }
        }
    }

    public void UpgradeClickDamage()
    {
        if (CurrencyManager.Instance != null && CurrencyManager.Instance.SpendCurrency(clickUpgradeCost))
        {
            
            clickDamage *= gameBalance.clickSettings.clickDamageMultiplier;
             
            clickUpgradeCost *= gameBalance.clickSettings.clickUpgradeCostMultiplier;

            UpdateClickStatsUI();
        }
        else
        {
            Debug.Log("������������ ������ ��� ��������� ����� �� �����");
            
        }
    }

    public void UpdateClickStatsUI()
    {
        if (clickDamageText != null)
        {
            clickDamageText.text = "���� �� �����: " + clickDamage.ToString("F0");
        }

        if (clickUpgradeCostText != null)
        {
            clickUpgradeCostText.text = "�������� ���� ��: " + clickUpgradeCost.ToString("F0");
        }
    }
}
