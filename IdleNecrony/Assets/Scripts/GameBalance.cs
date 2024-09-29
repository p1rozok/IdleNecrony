using UnityEngine;

[CreateAssetMenu(fileName = "GameBalance", menuName = "ScriptableObjects/GameBalance", order = 1)]
public class GameBalance : ScriptableObject
{
    [System.Serializable]
    public class UnitUpgradeSettings
    {
        [Tooltip("Базовая стоимость улучшения юнита")]
        public float baseUpgradeCost = 50f;
        [Tooltip("Множитель увеличения стоимости улучшения после каждого улучшения")]
        public float upgradeCostMultiplier = 1.5f;
        [Tooltip("Процент увеличения DPS при улучшении (от базового DPS юнита)")]
        public float dpsIncreaseMultiplier = 0.5f;
    }

    [System.Serializable]
    public class CurrencySettings
    {
        [Tooltip("Коэффициент преобразования урона в валюту")]
        public float currencyGenerationRate = 1f;
    }

    [System.Serializable]
    public class EnemySettings
    {
        [Tooltip("Базовое здоровье врага")]
        public float enemyBaseHealth = 100f;
        [Tooltip("Множитель увеличения здоровья врага после каждого появления")]
        public float enemyHealthMultiplier = 1.2f;
        [Tooltip("Базовое вознаграждение за уничтожение врага")]
        public float enemyBaseReward = 50f;
        [Tooltip("Множитель увеличения вознаграждения за каждого следующего врага")]
        public float enemyRewardMultiplier = 1.1f;
        [Tooltip("Скорость появления врагов (в секундах)")]
        public float enemySpawnInterval = 1f;
    }

    [System.Serializable]
    public class CastleSettings
    {
        [Tooltip("Здоровье замка")]
        public float castleHealth = 1000f;
    }

    [System.Serializable]
    public class ClickSettings
    {
        [Tooltip("Базовый урон от клика")]
        public float baseClickDamage = 1f;
        [Tooltip("Множитель увеличения урона от клика при улучшении")]
        public float clickDamageMultiplier = 1.5f;
        [Tooltip("Базовая стоимость улучшения урона от клика")]
        public float baseClickUpgradeCost = 100f;
        [Tooltip("Множитель увеличения стоимости улучшения урона от клика")]
        public float clickUpgradeCostMultiplier = 2f;
    }

    [Header("Параметры улучшения юнитов")]
    public UnitUpgradeSettings unitUpgradeSettings;

    [Header("Параметры генерации валюты")]
    public CurrencySettings currencySettings;

    [Header("Параметры врага")]
    public EnemySettings enemySettings;

    [Header("Параметры замка")]
    public CastleSettings castleSettings;

    [Header("Параметры клика")]
    public ClickSettings clickSettings;
}

