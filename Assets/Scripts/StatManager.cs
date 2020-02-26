using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatManager : MonoBehaviour
{

    public float Max_Health = 0f;
    public float Crash_Damage;
    public float Bullet_Damage;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI DeathDisplay;
    public TextMeshProUGUI cantAfford;

    public float ForwardVelocity;
    public float BackwardVelocity;
    public float RotSpeed;
    public float firing_rate;
    public float forward_projectile_spawn_height;
    public float Bullet_lifetime;
    public float Bullet_Velocity;

    [HideInInspector]
    public float health;
    private AudioManager AM;
    private PlayerHit PH;
    private CurrencyManager CM;

    private void Start()
    {
        if (tag.Equals("Player"))
        {
            PH = this.GetComponent<PlayerHit>();
            CM = this.GetComponent<CurrencyManager>();
        }
        AM = FindObjectOfType<AudioManager>();
        if (Max_Health <= 0)
            Debug.LogWarning("Max Health started at 0 or neg");
        health = Max_Health;
    }

    //Increases health by given amount
    public void heal(float amount)
    {
        if (amount < 0)
            Debug.LogWarning("Cannot heal by a negative amount!");
        health = Mathf.Min(health + amount, Max_Health);
    }

    //Decreases the health by a given amount
    public void take_damage(float amount)
    {
        if (tag.Equals("Player"))
        {
            AM.Play("PlayerHit");
            StartCoroutine(PH.ColorChange(PH.changeTime));
        }
        if (tag.Equals("Broad") || tag.Equals("SimpleEnemy"))
        {
            EnemyHit EH = this.GetComponent<EnemyHit>();
            StartCoroutine(EH.ColorChange(EH.changeTime));
            AM.Play("BroadHit");
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
        if (tag.Equals("SimpleEnemy") || tag.Equals("Broad"))
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
            healthDisplay.SetText(" " + health);
        }
        if (is_dead())
        {
            kill();
        }
    }

    public void inc_speed()
    {
        if (CM.balance >= 1)
        {
            ForwardVelocity++;
            BackwardVelocity++;
            CM.balance--;
        }
        else
        {
            Color newColor = cantAfford.color;
            newColor.a = 1;
            cantAfford.color = newColor;
        }
    }
    public void inc_damage()
    {
        if (CM.balance >= 1)
        {
            Bullet_Damage += 1;
            CM.balance--;
        }
        else
        {
            Color newColor = cantAfford.color;
            newColor.a = 1;
            cantAfford.color = newColor;
        }
    }
    public void inc_firing_cd()
    {
        if (CM.balance >= 1)
        {
            firing_rate -= .1f;
            CM.balance--;
        }
        else
        {
            Color newColor = cantAfford.color;
            newColor.a = 1;
            cantAfford.color = newColor;
        }
    }
    public void inc_health()
    {
        if (CM.balance >= 1)
        {
            Max_Health++;
            heal(1);
            CM.balance--;
        }
        else
        {
            Color newColor = cantAfford.color;
            newColor.a = 1;
            cantAfford.color = newColor;
        }
    }

}
