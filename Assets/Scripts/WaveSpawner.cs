using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public enum SpawnState { Spawning, Waiting, Counting, Reward}
        
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        // public int count; //number of enemies
        public float delay; //rate in which enemies spawn
    }

    public Wave[] waves;
    private int nextWave = 0;
    private int enemyNumber = 0;

    public Transform[] spawnPoints;
    public bool[] spawnPointsUsed;

    //For Random Occupied SpawnPoints
    ArrayList spawnList = new ArrayList();

    public Transform _sp;

    public Transform enemySpawned;
    public Bounds boundBox; //Added. For BoundBox for WanderState and EscapeState to be Precise. Seng
        
    public Transform[] rewardsList;
    
    public Transform[] rewardSpawnPoints;
    public int randNum;

    public GameObject tChestPooled;

    public float timeBetweenWaves = 1f;
    public float waveCountdown;

    private SpawnState state = SpawnState.Counting;

    private bool isAlive = true;
    private bool beginWaves = false;

    //Use Pooling System
    public bool isPooling = false;
    public GameObject enemyPooled;
    public PoolingManager poolingManager;

    void Start()
    {
        poolingManager = PoolingManager.instance;
        waveCountdown = timeBetweenWaves;

        ResetSpawnArrayList();
    }

    void Update()
    {
    if (beginWaves == true)
        {
            if (state == SpawnState.Waiting)
            {
                if (isAlive == false)
                {
                    BeginNewWave();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
                if (state == SpawnState.Reward)
                {
                    return;
                }
                else if (state != SpawnState.Spawning)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }
    public void EnemyCount(int count)
    {
        enemyNumber += count;

        if (enemyNumber <= 0)
        {
            EnemyIsAlive();
        }
    }

    void EnemyIsAlive()
    {
        isAlive = false;
    }

    void BeginNewWave()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;
        isAlive = true;

        if(nextWave + 1 > waves.Length - 1)
        {
            state = SpawnState.Reward;
            Debug.Log("All waves complete! Collect your rewards!");
              
            SpawnRewards();
 
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.Spawning;

        ResetSpawnArrayList();

        for (int i = 0; i < _wave.enemies.Length; i++) 
        {
            SpawnEnemy(_wave.enemies[i]);
            EnemyCount(1);
            yield return new WaitForSeconds(_wave.delay);
        }

        /*for (int i = 0; i < _wave.enemies.Length; i++)
        {
            spawnPointsUsed[i] = false;
        }*/

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {

        Debug.Log("Spawning Enemy: " + _enemy.name);
           
        if(spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }

        /*do
        {
            randNum = Random.Range(0, spawnPoints.Length);
            _sp = spawnPoints[randNum];
        } while (spawnPointsUsed[randNum]);

        spawnPointsUsed[randNum] = true;*/

        //For Random Enemy Spawn
        randNum = Random.Range(0, spawnList.Count);
        _sp = spawnPoints[(int)spawnList[randNum]];
        spawnList.RemoveAt(randNum);
        //

        /*for (int i = 0; i < spawnPoints.Length; i++) //Can only work with Number of Enemies Lower than Number of SpawnPoints. Also in Order;
        {
            if (spawnPointsUsed[spawnPointsUsed.Length - 1])
            {
                for (int j = 0; j < spawnPointsUsed.Length; j++)
                {
                    spawnPointsUsed[j] = false;
                }
            }

            if(!spawnPointsUsed[i])
            {
                _sp = spawnPoints[i];
                spawnPointsUsed[i] = true;
                break;
            }
        }*/

        if (!isPooling)
        {
            enemySpawned = Instantiate(_enemy, _sp.position, _sp.rotation); 
            enemySpawned.gameObject.GetComponent<EnemyBehaviour>().SetBoundBox(boundBox); //Added. To send BoundBox of Spawner to Enemy. Seng
            enemySpawned.gameObject.GetComponent<EnemyMain>().waveSpawnerObject = gameObject; //Added by Seng
        }
        else if (isPooling)
        {
            if (_enemy.gameObject.GetComponent<EnemyMelee>())
            {
                enemyPooled = poolingManager.callGoblinMelee();
            } 
            else if (_enemy.gameObject.GetComponent<EnemyRange>())
            {
                if (!_enemy.gameObject.GetComponent<EnemyRange>().isGhost)
                {
                    enemyPooled = poolingManager.callGoblinRange();
                }
                else if (_enemy.gameObject.GetComponent<EnemyRange>().isGhost)
                {
                    enemyPooled = poolingManager.callGhostRange();
                }
            }
            enemyPooled.transform.position = _sp.position;
            enemyPooled.transform.rotation = _sp.rotation;
            enemyPooled.GetComponent<EnemyMain>().InitializeEnemy();
            enemyPooled.GetComponent<EnemyMain>().waveSpawnerObject = gameObject;
            enemyPooled.GetComponent<EnemyMain>().isPooling = true;
            enemyPooled.GetComponent<EnemyBehaviour>().InitializeBehaviour();
            enemyPooled.GetComponent<EnemyBehaviour>().SetBoundBox(boundBox);
            enemyPooled.SetActive(true);
        }

    }

    void SpawnRewards ()
    {

        if (rewardSpawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }
        else
        {
            if (isPooling) //POOLING SYSTEM for TREASURE CHEST
            {
                for (int i = 0; i < rewardsList.Length; i++)
                {
                    Transform _sp = rewardSpawnPoints[i];
                    tChestPooled = poolingManager.callTreasureChest();
                    tChestPooled.SetActive(true);
                    tChestPooled.transform.position = _sp.position;
                    tChestPooled.transform.rotation = _sp.rotation;
                }
            }
            else
            {
                for (int i = 0; i < rewardsList.Length; i++)
                {
                    Transform _sp = rewardSpawnPoints[i];
                    Instantiate(rewardsList[i], _sp.position, _sp.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        beginWaves = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
    }

    public void ResetSpawnArrayList()
    {
        spawnList.Clear();
        spawnList.Add(0);
        spawnList.Add(1);
        spawnList.Add(2);
        spawnList.Add(3);
    }

    public void LoadData(GameData data)
    {
        if (!DataPersistenceManager.instance.newSceneLoading)
        {
            data.wavesSpawner.TryGetValue(id, out beginWaves);
            if (beginWaves)
            {
                this.gameObject.SetActive(false);
            }
        }

    }

    public void SaveData(GameData data)
    {
        if (data.wavesSpawner.ContainsKey(id))
        {
            data.wavesSpawner.Remove(id);
        }
        data.wavesSpawner.Add(id, beginWaves);
    }
}
