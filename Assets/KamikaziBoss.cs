using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikaziBoss : MonoBehaviour
{
    public GameObject bullet;
    public GameObject drone;
    public float bulletSpreadAngle;
    public int numBulletsEachSide;
    public float MaxVerticalSpeed;
    public float MinVerticalSpeed;
    public float minVerticalDistance;
    public float droneRate;
    public int numdrones;
    public GameObject spawn1;
    public GameObject spawn2;

    private float prevtime;
    private float lastfire;
    private float time;
    private GameObject player;
    private float SpawnHeight;
    private float VerticalSpeed;
    private StatManager PlayerSM;
    private StatManager SM;

    // Start is called before the first frame update
    private void Start()
    {
        prevtime = Time.time;
        VerticalSpeed = Random.Range(MinVerticalSpeed, MaxVerticalSpeed);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        SM = this.gameObject.GetComponent<StatManager>();
        PlayerSM = player.GetComponent<StatManager>();
        SpawnHeight = SM.forward_projectile_spawn_height;
    }

    void FixedUpdate()
    {
        if (transform.position.y >= minVerticalDistance)
        {
            Vector3 end = transform.position;
            end = end + (Vector3.down * VerticalSpeed);
            Vector3 start = transform.position;
            transform.position = Vector3.Lerp(start, end, Time.fixedDeltaTime);
            transform.right = player.transform.position - transform.position;
        }
        else
        {
            transform.right = player.transform.position - transform.position;
            if (Time.time > prevtime + droneRate)
            {
                for (int i = 0; i < numdrones/2; i++)
                {
                    GameObject obj1 = GameObject.Instantiate(drone, spawn1.transform.position, transform.rotation);
                    GameObject obj2 = GameObject.Instantiate(drone, spawn2.transform.position, transform.rotation);
                }
                prevtime = Time.time;
            }
        }
        time = Time.time;
        if (time > lastfire + SM.firing_rate)
        {
            Fire();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSM.take_damage(SM.Crash_Damage);
            SM.take_damage(PlayerSM.Crash_Damage);
        }
    }

    void Fire()
    {
        lastfire = time;
        Vector3 position = transform.position + transform.right * SpawnHeight;

        for (int i = 0; i < numBulletsEachSide; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            if (numBulletsEachSide % 2 == 1)
            {
                rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - (90 + (numBulletsEachSide / 2) * bulletSpreadAngle) + bulletSpreadAngle * i);
            }
            else
            {
                rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - (90 - (bulletSpreadAngle / 2) + ((numBulletsEachSide / 2) * bulletSpreadAngle)) + bulletSpreadAngle * i);
            }
            GameObject obj = GameObject.Instantiate(bullet, position, rotation);
            obj.GetComponent<EnemyBullet>().lifetime = SM.Bullet_lifetime;
            obj.GetComponent<EnemyBullet>().speed = SM.Bullet_Velocity;
            obj.GetComponent<EnemyBullet>().damage = SM.Bullet_Damage;
        }

        Vector3 pos = transform.position + transform.right * -SpawnHeight;
        for (int i = 0; i < numBulletsEachSide; i++)
        {
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            if (numBulletsEachSide % 2 == 1)
            {
                rot = Quaternion.Euler(0, 0, transform.eulerAngles.z + (90 + ((numBulletsEachSide / 2) * bulletSpreadAngle)) - bulletSpreadAngle * i);
            }
            else
            {
                rot = Quaternion.Euler(0, 0, transform.eulerAngles.z + (90 - (bulletSpreadAngle / 2) + ((numBulletsEachSide / 2) * bulletSpreadAngle)) - bulletSpreadAngle * i);
            }
            GameObject ob = GameObject.Instantiate(bullet, pos, rot);
            ob.GetComponent<EnemyBullet>().lifetime = SM.Bullet_lifetime;
            ob.GetComponent<EnemyBullet>().speed = SM.Bullet_Velocity;
            ob.GetComponent<EnemyBullet>().damage = SM.Bullet_Damage;
        }
    }
}
