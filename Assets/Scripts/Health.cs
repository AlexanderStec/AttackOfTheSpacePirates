using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    AudioManager AM;

    void Awake()
    {
        AM = FindObjectOfType<AudioManager>();
    }

        // Update is called once per frame
        void FixedUpdate()
    {
        if (this.gameObject.CompareTag("Player"))
            Debug.Log("Your current health is: " + health);
        if (health <= 0)
        {
            if(this.gameObject.tag == "Enemy")
            {
                AM.Play("EnemyDeath");
            }
            if (this.gameObject.tag == "Player")
            {
                AM.Play("PlayerDeath");
            }
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
    }
}
