using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;

    public float timebetweenspawns;
    public int NumSpawnRateIncrease;
    public float SpawnTimeDecrease;
    public float minSpawnTime;

    private GameObject enemy;
    private float spawntime;
    private int numSpawned;

    void Start()
    {
        spawntime = -timebetweenspawns;
        numSpawned = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        int spawnpoint = Random.Range(0, 10);
        if (spawntime + timebetweenspawns < Time.time)
        {
            int enemy_val = Random.Range(0, 2);
            enemy = enemies[enemy_val];
            spawntime = Time.time;
            GameObject.Instantiate(enemy, this.transform.GetChild(spawnpoint).position, Quaternion.identity);
            numSpawned++;
            if (numSpawned % NumSpawnRateIncrease == 0 && timebetweenspawns > .5f)
            {
                timebetweenspawns = timebetweenspawns - SpawnTimeDecrease;
                if (timebetweenspawns <= minSpawnTime)
                    timebetweenspawns = minSpawnTime;
            }
        }
    }
}
