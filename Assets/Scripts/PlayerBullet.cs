using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float lifetime;
    private float speed;
    private float spawntime;
    private int damage;
    private AudioManager AM;
    private GameObject player;
    private StatManager PlayerSM;

    // Start is called before the first frame update
    void Awake()
    {
        AM = FindObjectOfType<AudioManager>();
        spawntime = Time.time;
        AM.Play("PlayerBullet");
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerSM = player.GetComponent<StatManager>();
        lifetime = PlayerSM.Bullet_lifetime;
        speed = PlayerSM.Bullet_Velocity;
        damage = PlayerSM.Bullet_Damage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > spawntime + lifetime)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 direction = transform.up * speed;
            Vector3 start = transform.position;
            Vector3 end = transform.position + direction;
            transform.position = Vector3.Lerp(start, end, Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<StatManager>().take_damage(damage);
        Destroy(gameObject);
    }
}
