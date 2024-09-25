using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public float armyDPS = 0f; 
    public Text dpsText;

    public GameBalance gameBalance; 

    private List<Unit> units = new List<Unit>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (gameBalance == null)
        {
            Debug.LogError("GameBalance не привязан в ArmyManager");
            return;
        }

        units.AddRange(FindObjectsOfType<Unit>());

        foreach (Unit unit in units)
        {
            unit.OnDPSChanged += RecalculateArmyDPS;
        }

        RecalculateArmyDPS();
        StartCoroutine(DealDamageOverTime());
    }

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
        unit.OnDPSChanged += RecalculateArmyDPS;
        RecalculateArmyDPS();
    }

    public List<Unit> GetUnits()
    {
        return units;
    }

    void RecalculateArmyDPS()
    {
        armyDPS = 0f;
        foreach (Unit unit in units)
        {
            armyDPS += unit.currentDPS;
        }
        UpdateDPSUI();
    }

    void RecalculateArmyDPS(float unitDPS)
    {
        RecalculateArmyDPS();
    }

    void UpdateDPSUI()
    {
        if (dpsText != null)
        {
            dpsText.text = "Армия DPS: " + armyDPS.ToString("F0");
        }
    }

    IEnumerator DealDamageOverTime()
    {
        while (true)
        {
            float damage = armyDPS * Time.deltaTime;
            DealDamageToEnemy(damage);
            GenerateCurrency(damage);
            yield return null;
        }
    }

    void DealDamageToEnemy(float damage)
    {
        Enemy enemy = FindObjectOfType<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    void GenerateCurrency(float amount)
    {
        if (CurrencyManager.Instance != null)
        {
            float currencyAmount = amount * gameBalance.currencySettings.currencyGenerationRate;
            CurrencyManager.Instance.AddCurrency(currencyAmount);
        }
    }
}
