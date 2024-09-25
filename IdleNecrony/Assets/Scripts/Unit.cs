
using UnityEngine;
using UnityEngine.UI;
using System;

public class Unit : MonoBehaviour
{
    public string unitName;
    public float baseDPS = 1f;
    public float currentDPS;
    public float upgradeCost;

    public Text dpsText;
    public Text upgradeCostText;
    public Button upgradeButton;

    public GameBalance gameBalance; 

   
    public event Action<float> OnDPSChanged;

    private void Start()
    {
        if (gameBalance == null)
        {
            Debug.LogError("GameBalance не привязан в Unit: " + unitName);
            return;
        }

        currentDPS = baseDPS;
        upgradeCost = gameBalance.unitUpgradeSettings.baseUpgradeCost;
        UpdateUI();
        NotifyDPSChanged();
    }

    public void Upgrade()
    {
        if (CurrencyManager.Instance != null && CurrencyManager.Instance.SpendCurrency(upgradeCost))
        {
            
            float dpsIncrease = baseDPS * gameBalance.unitUpgradeSettings.dpsIncreaseMultiplier;
            currentDPS += dpsIncrease;

            
            upgradeCost *= gameBalance.unitUpgradeSettings.upgradeCostMultiplier;

            UpdateUI();
            NotifyDPSChanged();
        }
        else
        {
            Debug.Log("Недостаточно валюты для улучшения " + unitName);
            
        }
    }

    void UpdateUI()
    {
        if (dpsText != null)
        {
            dpsText.text = unitName + " DPS: " + currentDPS.ToString("F0");
        }
        if (upgradeCostText != null)
        {
            upgradeCostText.text = "Улучшить за: " + upgradeCost.ToString("F0");
        }
    }

    void NotifyDPSChanged()
    {
        OnDPSChanged?.Invoke(currentDPS);
    }
}
