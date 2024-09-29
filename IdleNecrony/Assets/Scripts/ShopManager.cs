using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    public GameObject shopItemPrefab;
    public Transform shopContent;

    public List<UnitData> unitsForSale;

    void Start()
    {
        PopulateShop();
    }

    void PopulateShop()
    {
        foreach (UnitData unitData in unitsForSale)
        {
            GameObject obj = Instantiate(shopItemPrefab, shopContent);
            obj.transform.Find("UnitNameText").GetComponent<Text>().text = unitData.unitName;
            Text costText = obj.transform.Find("UnitCostText").GetComponent<Text>();
            Button buyButton = obj.transform.Find("BuyButton").GetComponent<Button>();

            costText.text = "Стоимость: " + unitData.unitCost.ToString("F0");

            buyButton.onClick.AddListener(() =>
            {
                BuyUnit(unitData);
            });
        }
    }

    void BuyUnit(UnitData unitData)
    {
        if (CurrencyManager.Instance != null && CurrencyManager.Instance.SpendCurrency(unitData.unitCost))
        {

            GameObject unitObj = Instantiate(unitData.unitPrefab);
            Unit unit = unitObj.GetComponent<Unit>();
            unit.unitName = unitData.unitName;
            unit.baseDPS = unitData.baseDPS;
            unit.gameBalance = unitData.gameBalance;


            ArmyManager.Instance.AddUnit(unit);


        }
        else
        {
            Debug.Log("Недостаточно валюты для покупки " + unitData.unitName);
        }
    }
}
