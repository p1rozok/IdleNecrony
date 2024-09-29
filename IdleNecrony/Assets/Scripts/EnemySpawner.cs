

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
            Debug.Log("EnemySpawner установлен как Instance.");
        }
        else
        {
            Debug.LogError("EnemySpawner уже существует. Удаление дублирующегося экземпляра.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (gameBalance == null)
        {
            Debug.LogError("GameBalance не привязан в EnemySpawner");
            return;
        }

        Debug.Log("EnemySpawner успешно инициализирован.");
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
            Debug.LogError("Нет доступных префабов врагов.");
            return;
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject obj = Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        Enemy enemy = obj.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.gameBalance = gameBalance;
            enemy.Initialize(totalEnemiesDefeated);
            Debug.Log("Враг заспавнен.");
        }
        else
        {
            Debug.LogError("EnemyPrefab не содержит компонент Enemy");
        }
    }

    private void SpawnCastle()
    {
        if (currentCastleIndex >= castlePrefabs.Length)
        {
            Debug.LogError("Нет доступных префабов замков.");
            return;
        }

        int castleIndex = currentCastleIndex % castlePrefabs.Length;
        GameObject obj = Instantiate(castlePrefabs[castleIndex], spawnPoint.position, Quaternion.identity);
        Castle castle = obj.GetComponent<Castle>();
        if (castle != null)
        {
            castle.gameBalance = gameBalance;
            Debug.Log($"Создан замок с cutsceneIndex = {currentCastleIndex}");
        }
        else
        {
            Debug.LogError("CastlePrefab не содержит компонент Castle");
        }

        isCastleSpawned = true;
        currentCastleIndex++;
    }

    public void EnemyDefeated(bool isCastle, int cutsceneIndex)
    {
        if (isCastle)
        {
            Debug.Log("Замок уничтожен!");

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
            Debug.Log($"Враг уничтожен. Текущий счетчик уничтоженных врагов: {totalEnemiesDefeated}. В цикле: {enemiesInCurrentCycle}");
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
