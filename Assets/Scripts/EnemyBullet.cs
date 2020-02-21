using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector]
    public float lifetime;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int damage;

    private float spawntime;
    private AudioManager AM;
    private GameObject player;
    private StatManager PlayerSM;

    // Start is called before the first frame update
    void Awake()
    {
        AM = FindObjectOfType<AudioManager>();
        spawntime = Time.time;
        AM.Play("EnemyBullet");
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerSM = player.GetComponent<StatManager>();
        }
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
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerSM.take_damage(damage);
            Destroy(gameObject);
        }
        if (other.gameObject.tag.Equals("Bottom"))
        {
            Destroy(gameObject);
        }
    }
}
