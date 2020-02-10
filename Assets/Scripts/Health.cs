using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
    }
}
