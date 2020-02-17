using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    private float Horizontalspeed;
    private float VerticalSpeed;
    private bool Direction;
    public float MaxHorizontalSpeed;
    public float MaxVerticalSpeed;
    public float MinHorizontalSpeed;
    public float MinVerticalSpeed;
    private GameObject player;
    private Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = this.gameObject.GetComponent<Health>();
        Direction = (Random.value > .5f);
        Horizontalspeed = Random.Range(MinHorizontalSpeed, MaxHorizontalSpeed);
        VerticalSpeed = Random.Range(MinVerticalSpeed, MaxVerticalSpeed);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 end = transform.position;
        if (Direction)
        {
            end = end + (Vector3.right * Horizontalspeed);
        }
        else
        {
            end = end + (Vector3.left * Horizontalspeed);
        }

        end = end + (Vector3.down * VerticalSpeed);
        Vector3 start = transform.position;
        transform.position = Vector3.Lerp(start, end, Time.fixedDeltaTime);


    }

    internal void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
        {
            Direction = !Direction;
        }

        if (other.gameObject.CompareTag("Bottom"))
        {
            player.GetComponent<Health>().TakeDamage(1);
            FindObjectOfType<AudioManager>().Play("PlanetHit");
            health.TakeDamage(health.health);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Health>().TakeDamage(1);
            health.TakeDamage(health.health);
        }
    }
}
