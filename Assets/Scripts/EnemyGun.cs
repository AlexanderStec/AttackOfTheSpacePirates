using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private float lastfire;
    public GameObject prefab;
    private float SpawnHeight;
    public float minRange;
    public float maxRange;
    StatManager SM;

    // Start is called before the first frame update
    private void Start()
    {
        SM = this.gameObject.GetComponent<StatManager>();
        SpawnHeight = SM.forward_projectile_spawn_height;
    }

    void FixedUpdate()
    {
        float time = Time.time;
        if (time > lastfire + SM.firing_rate + Random.Range(minRange, maxRange))
        {
            lastfire = time;
            Vector3 position = transform.position + transform.up * -SpawnHeight;
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180);
            GameObject obj = GameObject.Instantiate(prefab, position, rotation);
            obj.GetComponent<EnemyBullet>().lifetime = SM.Bullet_lifetime;
            obj.GetComponent<EnemyBullet>().speed = SM.Bullet_Velocity;
            obj.GetComponent<EnemyBullet>().damage = SM.Bullet_Damage;
        }
    }
}
