

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public GameObject[] enemyPrefabs; 
    public GameObject[] castlePrefabs;

    public Transform spawnPoint;
    public int enemiesToSpawnBeforeCastle = 4;

    private int totalEnemiesDefeated = 0;
    private int enemiesInCurrentCycle = 0;
    private bool isCastleSpawned = false;
    private int currentCastleIndex = 0;

    public GameBalance gameBalance;
    public DialogSystem dialogSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("EnemySpawner ���������� ��� Instance.");
        }
        else
        {
            Debug.LogError("EnemySpawner ��� ����������. �������� �������������� ����������.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (gameBalance == null)
        {
            Debug.LogError("GameBalance �� �������� � EnemySpawner");
            return;
        }

        Debug.Log("EnemySpawner ������� ���������������.");
        SpawnNextEntity();
    }

    public void SpawnNextEntity()
    {
        if (!isCastleSpawned && enemiesInCurrentCycle < enemiesToSpawnBeforeCastle)
        {
            SpawnEnemy();
        }
        else if (!isCastleSpawned && enemiesInCurrentCycle >= enemiesToSpawnBeforeCastle)
        {
            SpawnCastle();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("��� ��������� �������� ������.");
            return;
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject obj = Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        Enemy enemy = obj.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.gameBalance = gameBalance;
            enemy.Initialize(totalEnemiesDefeated);
            Debug.Log("���� ���������.");
        }
        else
        {
            Debug.LogError("EnemyPrefab �� �������� ��������� Enemy");
        }
    }

    private void SpawnCastle()
    {
        if (currentCastleIndex >= castlePrefabs.Length)
        {
            Debug.LogError("��� ��������� �������� ������.");
            return;
        }

        int castleIndex = currentCastleIndex % castlePrefabs.Length;
        GameObject obj = Instantiate(castlePrefabs[castleIndex], spawnPoint.position, Quaternion.identity);
        Castle castle = obj.GetComponent<Castle>();
        if (castle != null)
        {
            castle.gameBalance = gameBalance;
            Debug.Log($"������ ����� � cutsceneIndex = {currentCastleIndex}");
        }
        else
        {
            Debug.LogError("CastlePrefab �� �������� ��������� Castle");
        }

        isCastleSpawned = true;
        currentCastleIndex++;
    }

    public void EnemyDefeated(bool isCastle, int cutsceneIndex)
    {
        if (isCastle)
        {
            Debug.Log("����� ���������!");

            isCastleSpawned = false;
            enemiesInCurrentCycle = 0;

            if (dialogSystem != null)
            {
                dialogSystem.StartDialog();
            }
            else
            {
                SpawnNextEntity();
            }
        }
        else
        {
            totalEnemiesDefeated++;
            enemiesInCurrentCycle++;
            Debug.Log($"���� ���������. ������� ������� ������������ ������: {totalEnemiesDefeated}. � �����: {enemiesInCurrentCycle}");
            SpawnNextEntity();
        }
    }

    public void ResumeGameAfterDialog()
    {
        SpawnNextEntity();
    }

    public int GetDefeatedEnemiesCount()
    {
        return totalEnemiesDefeated;
    }
}
