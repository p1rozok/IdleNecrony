using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitUpgradeManager : MonoBehaviour
{
    public Transform unitsContent;
    public GameObject unitUpgradeItemPrefab;

    private List<Unit> units = new List<Unit>();

    private void Start()
    {
        units = ArmyManager.Instance.GetUnits();
        PopulateUnits();
    }

    void PopulateUnits()
    {
        foreach (Unit unit in units)
        {
            GameObject obj = Instantiate(unitUpgradeItemPrefab, unitsContent);
            obj.transform.Find("UnitNameText").GetComponent<Text>().text = unit.unitName;
            Text dpsText = obj.transform.Find("UnitDPSText").GetComponent<Text>();
            Text upgradeCostText = obj.transform.Find("UpgradeCostText").GetComponent<Text>();
            Button upgradeButton = obj.transform.Find("UpgradeButton").GetComponent<Button>();

            UpdateUnitUI(unit, dpsText, upgradeCostText);

            upgradeButton.onClick.AddListener(() =>
            {
                unit.Upgrade();
                UpdateUnitUI(unit, dpsText, upgradeCostText);
            });
        }
    }

    void UpdateUnitUI(Unit unit, Text dpsText, Text upgradeCostText)
    {
        dpsText.text = "DPS: " + unit.currentDPS.ToString("F0");
        upgradeCostText.text = "Улучшить за: " + unit.upgradeCost.ToString("F0");
    }
}


 