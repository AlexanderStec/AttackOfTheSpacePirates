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
            Vector3 direction = transform.up * speed;
            Vector3 start = transform.position;
            Vector3 end = transform.position + direction;
            transform.position = Vector3.Lerp(start, end, Time.fixedDeltaTime);
        }
    }
    internal void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
