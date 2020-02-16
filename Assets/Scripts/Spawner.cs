using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float timebetweenspawns;
    private float spawntime;
    private int numSpawned;
    public int NumSpawnRateIncrease;
    public float SpawnTimeDecrease;

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
            spawntime = Time.time;
            GameObject.Instantiate(enemy, this.transform.GetChild(spawnpoint).position, Quaternion.identity);
            numSpawned++;
            if (numSpawned % NumSpawnRateIncrease == 0 && timebetweenspawns > .5f)
            {
                timebetweenspawns = timebetweenspawns - SpawnTimeDecrease;
                if (timebetweenspawns <= 0)
                    timebetweenspawns = .25f;
            }
        }
    }
}
