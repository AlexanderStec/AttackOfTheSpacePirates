using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        [System.Serializable]
        public class Enemies
        {
            public bool boss;
            public string bosstext;
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
    public int levelReward;
    
    //implement force spawning!
    public bool waitForAllEnemiesDead;
    public float FirstWaveStartTime;
    public float forceSpawnNextWave;
    public TextMeshProUGUI complete;
    public Image shop;
    public TextMeshProUGUI bosshealth;
    public Image bosshealthbar;

    private float forceSpawnCountDown;
    private float waveCountDown;
    private SpawnState state = SpawnState.COUNTING;
    private float searchCountdown = 1f;
    private GameObject player;


    private void Start()
    {
        numSpawnpoints = GameObject.FindGameObjectWithTag("Spawner").transform.childCount;
        waveCountDown = FirstWaveStartTime;
        forceSpawnCountDown = forceSpawnNextWave;
        player = GameObject.FindGameObjectWithTag("Player");
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
            player.GetComponent<CurrencyManager>().addMoney(levelReward);
            complete.gameObject.SetActive(true);
            bosshealth.gameObject.SetActive(false);
            StartCoroutine(OpenShop());
            //this is where you write code for when you defeat all waves (delete above line)
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
                if (E.boss)
                {
                    bosshealth.gameObject.SetActive(true);
                    bosshealth.text = E.bosstext;
                    SpawnEnemy(E.enemy, true);
                }
                else
                    SpawnEnemy(E.enemy, false);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        state = SpawnState.WAITING;

        yield break;
    }

    IEnumerator Bosshptrack(GameObject enemy)
    {
        StatManager sm = enemy.GetComponent<StatManager>();
        yield return new WaitForSeconds(1f);
        while (sm.health >= 0f)
        {
            bosshealthbar.rectTransform.localScale = new Vector3((sm.health / sm.Max_Health), 1f, 1f);
            yield return null;
        }
    }


    IEnumerator OpenShop()
    {
        yield return new WaitForSeconds(2f);
        shop.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void SpawnEnemy(GameObject enemy, bool boss)
    {
        int spawnpoint = Random.Range(0, numSpawnpoints);
        GameObject obj = GameObject.Instantiate(enemy, this.transform.GetChild(spawnpoint).position, Quaternion.identity);
        if (boss)
        {
            StartCoroutine(Bosshptrack(obj));
        }
    }

}
