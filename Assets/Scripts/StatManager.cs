using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatManager : MonoBehaviour
{

    public int Max_Health = 0;
    public int Crash_Damage;
    public int Bullet_Damage;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI DeathDisplay;

    public float ForwardVelocity;
    public float BackwardVelocity;
    public float RotSpeed;
    public float firing_rate;
    public float forward_projectile_spawn_height;
    public float Bullet_lifetime;
    public float Bullet_Velocity;

    private int health;
    private AudioManager AM;
    private PlayerHit PH;

    private void Start()
    {
        if (tag.Equals("Player"))
            PH = this.GetComponent<PlayerHit>();
        AM = FindObjectOfType<AudioManager>();
        if (Max_Health <= 0)
            Debug.LogWarning("Max Health started at 0 or neg");
        health = Max_Health;
    }

    //Increases health by given amount
    public void heal(int amount)
    {
        if (amount < 0)
            Debug.LogWarning("Cannot heal by a negative amount!");
        health = Mathf.Min(health + amount, Max_Health);
    }

    //Decreases the health by a given amount
    public void take_damage(int amount)
    {
        if (tag.Equals("Player"))
        {
            AM.Play("PlayerHit");
            StartCoroutine(PH.ColorChange(PH.changeTime));
        }
        if (amount < 0)
            Debug.LogWarning("Cannot cause negative damage!");
        health = Mathf.Max(health - amount, 0);
    }

    //Returns if the ship should be killed
    public bool is_dead()
    {
        return health <= 0;
    }

    public void kill()
    {
        if (tag.Equals("SimpleEnemy"))
            AM.Play("SimpleEnemyDeath");
        if (tag.Equals("Player"))
        {
            AM.Play("PlayerDeath");
            DeathDisplay.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Spawner").SetActive(false);
        }
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (this.gameObject.tag.Equals("Player"))
        {
            healthDisplay.SetText(":" + health);
        }
        if (is_dead())
        {
            kill();
        }
    }

}
