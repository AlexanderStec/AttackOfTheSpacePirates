using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public float speed;
    private float spawntime;
    public int damage;
    private AudioManager AM;

    // Start is called before the first frame update
    void Awake()
    {
        AM = FindObjectOfType<AudioManager>();
        spawntime = Time.time;
        AM.Play("PlayerBullet");
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
            Vector2 direction = transform.up * speed;
            Vector3 move = direction * Time.fixedDeltaTime;
            transform.position = transform.position + move;
        }
    }
    internal void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
