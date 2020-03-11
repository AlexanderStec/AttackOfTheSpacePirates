using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatManager : MonoBehaviour
{
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI DeathDisplay;
    public TextMeshProUGUI cantAfford;
    public TextMeshProUGUI maxUpgrade;
    public TextMeshProUGUI healthupgradecost;
    public TextMeshProUGUI healthupgradetext;
    public TextMeshProUGUI movementupgradecost;
    public TextMeshProUGUI movementupgradetext;
    public TextMeshProUGUI numbulletupgradecost;
    public TextMeshProUGUI numbulletupgradetext;
    public TextMeshProUGUI bulletvelocityupgradecost;
    public TextMeshProUGUI bulletvelocityupgradetext;
    public TextMeshProUGUI bulletdamageupgradecost;
    public TextMeshProUGUI bulletdamageupgradetext;
    public TextMeshProUGUI FiringCooldownupgradecost;
    public TextMeshProUGUI FiringCooldownupgradetext;

    public float Max_Health;
    public float Crash_Damage;
    public float Bullet_Damage;
    public float ForwardVelocity;
    public float BackwardVelocity;
    public float RotSpeed;
    public float firing_rate;
    public float forward_projectile_spawn_height;
    public float Bullet_lifetime;
    public float Bullet_Velocity;
    public int num_bullet;
    public int moneydropped;

    [HideInInspector]
    public float health;

    [HideInInspector]
    public float healthupgrade = 0;
    [HideInInspector]
    public float movementupgrade = 0;
    [HideInInspector]
    public float bulletvelocityupgrade = 0;
    [HideInInspector]
    public float numbulletupgrade = 0;
    [HideInInspector]
    public float bulletdamageupgrade = 0;
    [HideInInspector]
    public float firingcdupgrade = 0;

    private AudioManager AM;
    private PlayerHit PH;
    private CurrencyManager CM;

    private void Start()
    {
        health = Max_Health;
        if (tag.Equals("Player"))
        {
            PH = this.GetComponent<PlayerHit>();
            CM = this.GetComponent<CurrencyManager>();
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
                CM = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyManager>();
        }
        AM = FindObjectOfType<AudioManager>();
        if (Max_Health <= 0)
            Debug.LogWarning("Max Health started at 0 or neg");
        if (!SceneManager.GetActiveScene().name.Equals("Level 1") && tag.Equals("Player")) //if not on first level and player is in scene this runs
        {
            StatManager sk = Data.CurrentStats;
            Max_Health = sk.Max_Health;
            Crash_Damage = sk.Crash_Damage;
            Bullet_Damage = sk.Bullet_Damage;
            ForwardVelocity = sk.ForwardVelocity;
            BackwardVelocity = sk.BackwardVelocity;
            RotSpeed = sk.RotSpeed;
            firing_rate = sk.firing_rate;
            forward_projectile_spawn_height = sk.forward_projectile_spawn_height;
            Bullet_lifetime = sk.Bullet_lifetime;
            Bullet_Velocity = sk.Bullet_Velocity;
            num_bullet = sk.num_bullet;

            health = Max_Health;

            healthupgrade = sk.healthupgrade;
            movementupgrade = sk.movementupgrade;
            bulletvelocityupgrade = sk.bulletvelocityupgrade;
            numbulletupgrade = sk.numbulletupgrade;
            bulletdamageupgrade = sk.bulletdamageupgrade;
            firingcdupgrade = sk.firingcdupgrade;
        }
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
            if (health > 0f)
            {
                EnemyHit EH = this.GetComponent<EnemyHit>();
                StartCoroutine(EH.ColorChange(EH.changeTime));
                AM.Play("BroadHit");
            }
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
        {
            AM.Play("SimpleEnemyDeath");
            CM.addMoney(moneydropped);
        }
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
            healthDisplay.SetText(" " + health + "/" + Max_Health);
            if (healthupgrade < 5)
            {
                if (healthupgrade == 1 || healthupgrade == 0)
                    healthupgradetext.SetText("MAX HEALTH +" + 1);
                if (healthupgrade == 2 || healthupgrade == 3)
                    healthupgradetext.SetText("MAX HEALTH +" + 2);
                if (healthupgrade == 4)
                    healthupgradetext.SetText("MAX HEALTH +" + 3);
                healthupgradecost.SetText("Cost: " + +Mathf.Pow(2, healthupgrade) * 10 + " ZBK");
            }
            if (healthupgrade == 5)
            {
                healthupgradetext.SetText("MAXED");
                healthupgradecost.SetText("OUT");
            }
            if (movementupgrade < 5)
            {
                if (movementupgrade <=3)
                    movementupgradetext.SetText("Movement Speed \nForwards Velocity +" + .5 + "m/s" + "\nBackwards Velocity +" + .25 + "m/s");
                if (movementupgrade == 4)
                    movementupgradetext.SetText("Movement Speed \nForwards Velocity +" + 1 + "m/s" + "\nBackwards Velocity +" + .5 + "m/s");
                movementupgradecost.SetText("Cost: " + +Mathf.Pow(2, movementupgrade) * 10 + " ZBK");
            }
            if (movementupgrade == 5)
            {
                movementupgradetext.SetText("\n\nMAXED");
                movementupgradecost.SetText("OUT");
            }
            if (numbulletupgrade < 5)
            {
                numbulletupgradetext.SetText("Num Bullets +" + 1 +"\nFiring Rate +0.1s");
                numbulletupgradecost.SetText("Cost: " + +Mathf.Pow(2, numbulletupgrade) * 10 + " ZBK");
            }
            if (numbulletupgrade == 5)
            {
                numbulletupgradetext.SetText("\nMAXED");
                numbulletupgradecost.SetText("OUT");
            }
            if (bulletvelocityupgrade < 5)
            {
                if (bulletvelocityupgrade <= 3)
                    bulletvelocityupgradetext.SetText("Bullet Velocity +" + .5 + "m/s");
                if (bulletvelocityupgrade == 4)
                    bulletvelocityupgradetext.SetText("Bullet Velocity +" + 1 + "m/s");
                bulletvelocityupgradecost.SetText("Cost: " + +Mathf.Pow(2, bulletvelocityupgrade) * 10 + " ZBK");
            }
            if (bulletvelocityupgrade == 5)
            {
                bulletvelocityupgradetext.SetText("MAXED");
                bulletvelocityupgradecost.SetText("OUT");
            }
            if (firingcdupgrade < 5)
            {
                FiringCooldownupgradetext.SetText("Firing Cooldown -" + .05 + "s");
                FiringCooldownupgradecost.SetText("Cost: " + +Mathf.Pow(2, firingcdupgrade) * 10 + " ZBK");
            }
            if (firingcdupgrade == 5)
            {
                FiringCooldownupgradetext.SetText("MAXED");
                FiringCooldownupgradecost.SetText("OUT");
            }
            if (bulletdamageupgrade < 5)
            {
                if (bulletdamageupgrade == 2 || bulletdamageupgrade == 1 || bulletdamageupgrade == 0)
                    bulletdamageupgradetext.SetText("Bullet Damage +" + .5);
                if (bulletdamageupgrade == 4 || bulletdamageupgrade == 3)
                    bulletdamageupgradetext.SetText("Bullet Damage +" + 1);
                bulletdamageupgradecost.SetText("Cost: " + +Mathf.Pow(2, bulletdamageupgrade) * 10 + " ZBK");
            }
            if (bulletdamageupgrade == 5)
            {
                bulletdamageupgradetext.SetText("MAXED");
                bulletdamageupgradecost.SetText("OUT");
            }
        }
        if (is_dead())
        {
            kill();
        }
        Data.CurrentStats = this.GetComponent<StatManager>();
    }

    public void inc_speed()
    {
        if (movementupgrade < 5)
        {
            float cost = Mathf.Pow(2, movementupgrade) * 10;
            if (CM.balance >= cost)
            {
                if (movementupgrade < 4)
                {
                    ForwardVelocity += .5f;
                    BackwardVelocity += .25f;
                }
                else
                {
                    ForwardVelocity += 1f;
                    BackwardVelocity += .5f;
                }
                CM.removeMoney((int)cost);
                movementupgrade++;
            }
            else
            {
                Color newColor = cantAfford.color;
                newColor.a = 1;
                cantAfford.color = newColor;
            }
        }
        else
        {
            Color newColor = maxUpgrade.color;
            newColor.a = 1;
            maxUpgrade.color = newColor;
        }
    }
    public void inc_damage()
    {
        if (bulletdamageupgrade < 5)
        {
            float cost = Mathf.Pow(2, bulletdamageupgrade) * 10;
            if (CM.balance >= cost)
            {
                if (bulletdamageupgrade < 3)
                {
                    Bullet_Damage += .5f;
                }
                else
                {
                    Bullet_Damage += 1f;
                }
                CM.removeMoney((int)cost);
                bulletdamageupgrade++;
            }
            else
            {
                Color newColor = cantAfford.color;
                newColor.a = 1;
                cantAfford.color = newColor;
            }
        }
        else
        {
            Color newColor = maxUpgrade.color;
            newColor.a = 1;
            maxUpgrade.color = newColor;
        }
    }
    public void inc_firing_cd()
    {
        if (firingcdupgrade < 5)
        {
            float cost = Mathf.Pow(2, firingcdupgrade) * 10;
            if (CM.balance >= cost)
            {
                firing_rate-=.05f;
                CM.removeMoney((int)cost);
                firingcdupgrade++;
            }
            else
            {
                Color newColor = cantAfford.color;
                newColor.a = 1;
                cantAfford.color = newColor;
            }
        }
        else
        {
            Color newColor = maxUpgrade.color;
            newColor.a = 1;
            maxUpgrade.color = newColor;
        }
    }
    public void inc_health()
    {
        if (healthupgrade < 5)
        {
            float cost = Mathf.Pow(2, healthupgrade) * 10;
            if (CM.balance >= cost)
            {
                if (healthupgrade == 0)
                {
                    Max_Health++;
                    heal(1);
                }
                if (healthupgrade == 1)
                {
                    Max_Health++;
                    heal(1);
                }
                if (healthupgrade == 2)
                {
                    Max_Health+= 2f;
                    heal(2);
                }
                if (healthupgrade == 3)
                {
                    Max_Health += 2f;
                    heal(2);
                }
                if (healthupgrade == 4)
                {
                    Max_Health += 3f;
                    heal(3);
                }

                CM.removeMoney((int)cost);
                healthupgrade++;
            }
            else
            {
                Color newColor = cantAfford.color;
                newColor.a = 1;
                cantAfford.color = newColor;
            }
        }
        else
        {
            Color newColor = maxUpgrade.color;
            newColor.a = 1;
            maxUpgrade.color = newColor;
        }
    }
    public void inc_bullet_velocity()
    {
        if (bulletvelocityupgrade < 5)
        {
            float cost = Mathf.Pow(2, bulletvelocityupgrade) * 10;
            if (CM.balance >= cost)
            {
                if (bulletvelocityupgrade < 4)
                {
                    Bullet_Velocity += .5f;
                }
                else
                {
                    Bullet_Velocity += 1f;
                }
                CM.removeMoney((int)cost);
                bulletvelocityupgrade++;
            }
            else
            {
                Color newColor = cantAfford.color;
                newColor.a = 1;
                cantAfford.color = newColor;
            }
        }
        else
        {
            Color newColor = maxUpgrade.color;
            newColor.a = 1;
            maxUpgrade.color = newColor;
        }
    }
    public void inc_num_bullets()
    {
        if (numbulletupgrade < 5)
        {
            float cost = Mathf.Pow(2, numbulletupgrade) * 10;
            if (CM.balance >= cost)
            {
                num_bullet++;
                firing_rate += .1f;
                CM.removeMoney((int)cost);
                numbulletupgrade++;
            }
            else
            {
                Color newColor = cantAfford.color;
                newColor.a = 1;
                cantAfford.color = newColor;
            }
        }
        else
        {
            Color newColor = maxUpgrade.color;
            newColor.a = 1;
            maxUpgrade.color = newColor;
        }
    }

}
