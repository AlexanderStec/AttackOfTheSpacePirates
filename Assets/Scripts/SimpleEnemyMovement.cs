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
    // Start is called before the first frame update
    void Start()
    {
        Direction = (Random.value > .5f);
        Horizontalspeed = Random.Range(MinHorizontalSpeed, MaxHorizontalSpeed);
        VerticalSpeed = Random.Range(MinVerticalSpeed, MaxVerticalSpeed);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        if (Direction)
        {
            move = move + ((Vector3.right * Horizontalspeed) * Time.fixedDeltaTime);
        }
        else
        {
            move = move + ((Vector3.left * Horizontalspeed) * Time.fixedDeltaTime);
        }

        move = move + ((Vector3.down * VerticalSpeed) * Time.fixedDeltaTime);
        transform.position = transform.position + move;


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
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Health>().TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
