using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Prefabs")]
    public GameObject[] enemyPrefabs; 
    public GameObject[] castlePrefabs; 

    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public int enemiesToSpawnBeforeCastle = 4; 

    private int totalEnemiesDefeated = 0; 
    private int enemiesInCurrentCycle = 0; 
    private bool isCastleSpawned = false; 
    private int currentCastleIndex = 0; 

    public GameBalance gameBalance; 

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

        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("EnemySpawner не содержит ни одного префаба врага!");
            return;
        }

        if (castlePrefabs.Length == 0)
        {
            Debug.LogError("EnemySpawner не содержит ни одного префаба замка!");
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
        
        else if (isCastleSpawned)
        {
            Debug.Log("Продолжаем спавн врагов после уничтожения замка.");
            isCastleSpawned = false;
            enemiesInCurrentCycle = 0; 
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
       
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject obj = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.identity);
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
            Debug.LogError("Нет доступных префабов замков для спавна.");
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
            SpawnNextEntity();
        }
        else
        {
            totalEnemiesDefeated++;
            enemiesInCurrentCycle++;
            Debug.Log($"Враг уничтожен. Текущий счетчик уничтоженных врагов: {totalEnemiesDefeated}. В цикле: {enemiesInCurrentCycle}");
            SpawnNextEntity();
        }
    }

    public int GetDefeatedEnemiesCount()
    {
        return totalEnemiesDefeated;
    }
}
