using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    public float MaxHorizontalSpeed;
    public float MaxVerticalSpeed;
    public float MinHorizontalSpeed;
    public float MinVerticalSpeed;

    private float Horizontalspeed;
    private float VerticalSpeed;
    private bool Direction;
    private GameObject player;
    private StatManager PlayerSM;
    private StatManager EnemySM;
    // Start is called before the first frame update
    void Start()
    {
        Direction = (Random.value > .5f);
        Horizontalspeed = Random.Range(MinHorizontalSpeed, MaxHorizontalSpeed);
        VerticalSpeed = Random.Range(MinVerticalSpeed, MaxVerticalSpeed);
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerSM = player.GetComponent<StatManager>();
        EnemySM = GetComponent<StatManager>();
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
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("SimpleEnemy"))
        {
            Direction = !Direction;
        }

        if (other.gameObject.CompareTag("Bottom"))
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                PlayerSM.take_damage(EnemySM.Crash_Damage);
            }
            EnemySM.take_damage(EnemySM.Max_Health);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSM.take_damage(EnemySM.Crash_Damage);
            EnemySM.take_damage(PlayerSM.Crash_Damage);
        }
    }
}
