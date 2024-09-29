using UnityEngine;

[CreateAssetMenu(fileName = "GameBalance", menuName = "ScriptableObjects/GameBalance", order = 1)]
public class GameBalance : ScriptableObject
{
    [System.Serializable]
    public class UnitUpgradeSettings
    {
        [Tooltip("������� ��������� ��������� �����")]
        public float baseUpgradeCost = 50f;
        [Tooltip("��������� ���������� ��������� ��������� ����� ������� ���������")]
        public float upgradeCostMultiplier = 1.5f;
        [Tooltip("������� ���������� DPS ��� ��������� (�� �������� DPS �����)")]
        public float dpsIncreaseMultiplier = 0.5f;
    }

    [System.Serializable]
    public class CurrencySettings
    {
        [Tooltip("����������� �������������� ����� � ������")]
        public float currencyGenerationRate = 1f;
    }

    [System.Serializable]
    public class EnemySettings
    {
        [Tooltip("������� �������� �����")]
        public float enemyBaseHealth = 100f;
        [Tooltip("��������� ���������� �������� ����� ����� ������� ���������")]
        public float enemyHealthMultiplier = 1.2f;
        [Tooltip("������� �������������� �� ����������� �����")]
        public float enemyBaseReward = 50f;
        [Tooltip("��������� ���������� �������������� �� ������� ���������� �����")]
        public float enemyRewardMultiplier = 1.1f;
        [Tooltip("�������� ��������� ������ (� ��������)")]
        public float enemySpawnInterval = 1f;
    }

    [System.Serializable]
    public class CastleSettings
    {
        [Tooltip("�������� �����")]
        public float castleHealth = 1000f;
    }

    [System.Serializable]
    public class ClickSettings
    {
        [Tooltip("������� ���� �� �����")]
        public float baseClickDamage = 1f;
        [Tooltip("��������� ���������� ����� �� ����� ��� ���������")]
        public float clickDamageMultiplier = 1.5f;
        [Tooltip("������� ��������� ��������� ����� �� �����")]
        public float baseClickUpgradeCost = 100f;
        [Tooltip("��������� ���������� ��������� ��������� ����� �� �����")]
        public float clickUpgradeCostMultiplier = 2f;
    }

    [Header("��������� ��������� ������")]
    public UnitUpgradeSettings unitUpgradeSettings;

    [Header("��������� ��������� ������")]
    public CurrencySettings currencySettings;

    [Header("��������� �����")]
    public EnemySettings enemySettings;

    [Header("��������� �����")]
    public CastleSettings castleSettings;

    [Header("��������� �����")]
    public ClickSettings clickSettings;
}

