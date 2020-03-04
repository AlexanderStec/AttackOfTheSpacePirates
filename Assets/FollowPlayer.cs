using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private float speed;
    private StatManager PlayerSM;
    private StatManager EnemySM;
    // Start is called before the first frame update
    void Start()
    {
        EnemySM = this.GetComponent<StatManager>();
        speed = EnemySM.ForwardVelocity;
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerSM = player.GetComponent<StatManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            transform.right = player.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    internal void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSM.take_damage(EnemySM.Crash_Damage);
            EnemySM.take_damage(PlayerSM.Crash_Damage);
        }
    }
}
