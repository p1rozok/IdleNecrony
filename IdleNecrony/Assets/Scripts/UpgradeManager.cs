using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class UpgradeManager : MonoBehaviour
{
    
    public Text clickUpgradeCostText;
    public Button clickUpgradeButton;

    public GameBalance gameBalance;
    public PlayerClick playerClick;

    private void Start()
    {
        UpdateUI();
        clickUpgradeButton.onClick.AddListener(UpgradeClickDamage);
    }

    void UpgradeClickDamage()
    {
        if (CurrencyManager.Instance.SpendCurrency(playerClick.clickUpgradeCost))
        {
            
            playerClick.clickDamage *= gameBalance.clickSettings.clickDamageMultiplier; 
            playerClick.clickUpgradeCost *= gameBalance.clickSettings.clickUpgradeCostMultiplier;
            UpdateUI();
        }
        else
        {
            Debug.Log("������������ ������ ��� ��������� ����� �� �����");
        }
    }

    void UpdateUI()
    {
        if (clickUpgradeCostText != null)
        {
            clickUpgradeCostText.text = "�������� ���� ��: " + playerClick.clickUpgradeCost.ToString("F0");
        }
    }
}
