using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        [System.Serializable]
        public class Enemies
        {
            public GameObject enemy;
            public int count;
        }
        public string name;
        public float rate;
        public Enemies[] enemies;

    }
    public Wave[] waves;
    private int numSpawnpoints;
    private int nextWave = 0;
    public float timeBetweenWaves;
    
    //implement force spawning!
    public bool waitForAllEnemiesDead;
    public float FirstWaveStartTime;
    public float forceSpawnNextWave;

    private float forceSpawnCountDown;
    private float waveCountDown;
    private SpawnState state = SpawnState.COUNTING;
    private float searchCountdown = 1f;

    private void Start()
    {
        numSpawnpoints = GameObject.FindGameObjectWithTag("Spawner").transform.childCount;
        waveCountDown = FirstWaveStartTime;
        forceSpawnCountDown = forceSpawnNextWave;
    }
    private void FixedUpdate()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.fixedDeltaTime;
        }
    }
    public bool EnemyIsAlive()
    {
        if (waitForAllEnemiesDead)
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (GameObject.FindGameObjectWithTag("SimpleEnemy") == null && GameObject.FindGameObjectWithTag("Broad") == null)
                {
                    return false;
                }
            }
        }
        else
        {
            forceSpawnCountDown -= Time.fixedDeltaTime;
            if (forceSpawnCountDown <= 0f)
                return false;
        }
        return true;
    }
    public void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        if (forceSpawnCountDown <= 0f)
            waveCountDown = 0f;
        else
            waveCountDown = timeBetweenWaves;
        forceSpawnCountDown = forceSpawnNextWave;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete!");
            //this is where you add wave complete code
        }
        else
        {
            nextWave++;
        }
    }
    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        foreach (var E in wave.enemies)
        {
            for (int i = 0; i < E.count; i++)
            {
                SpawnEnemy(E.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject enemy)
    {
        int spawnpoint = Random.Range(0, numSpawnpoints);
        GameObject.Instantiate(enemy, this.transform.GetChild(spawnpoint).position, Quaternion.identity);
    }

}
