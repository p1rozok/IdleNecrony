using UnityEngine;
using UnityEngine.UI;


public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public float currency = 0f;
    public Text currencyText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateCurrencyUI();
    }

    public void AddCurrency(float amount)
    {
        currency += amount;
        UpdateCurrencyUI();
    }

    public bool SpendCurrency(float amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            UpdateCurrencyUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = "Валюта: " + Mathf.FloorToInt(currency);
        }
    }
}
