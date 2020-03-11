using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AbilityManager : MonoBehaviour
{

    public float ShieldCooldown;
    public float ShieldUpTime;

    public float DashDuration;
    public float DashCD;
    public float DashDistance;

    public GameObject leftsmoke;

    public GameObject rightsmoke;

    public GameObject shield;
    public Image ShieldCD;

    public Image LeftDashImage;
    public Image RightDashImage;

    public TextMeshProUGUI cantAfford;
    public TextMeshProUGUI maxUpgrade;

    private bool ShieldOnCD;
    private bool LeftDash;
    private bool RightDash;
    private CurrencyManager CM;

    [HideInInspector]
    public float shieldcdupgrade = 0;
    [HideInInspector]
    public float shielddurationupgrade = 0;
    [HideInInspector]
    public float dashdistanceupgrade = 0;
    [HideInInspector]
    public float dashcdupgrade = 0;

    private void Start()
    {

        ShieldOnCD = false;
        LeftDash = false;
        RightDash = false;
        CM = this.GetComponent<CurrencyManager>();
        if (!SceneManager.GetActiveScene().name.Equals("Level 1") && tag.Equals("Player")) //if not on first level and player is in scene this runs
        {
            AbilityManager sk = Data.CurrentAbilities;
            ShieldCooldown = sk.ShieldCooldown;
            ShieldUpTime = sk.ShieldUpTime;
            DashDuration = sk.DashDuration;
            DashCD = sk.DashCD;
            DashDistance = sk.DashDistance;

            shieldcdupgrade = sk.shieldcdupgrade;
            shielddurationupgrade = sk.shielddurationupgrade;
            dashdistanceupgrade = sk.dashdistanceupgrade;
            dashcdupgrade = sk.dashcdupgrade;
        }
    }
    private void FixedUpdate()
    {
        Data.CurrentAbilities = this.GetComponent<AbilityManager>();
        ShieldUpdate();
        LeftUpdate();
        RightUpdate();
    }

    private void LeftUpdate()
    {
        if (LeftDash)
        {
            LeftDashImage.fillAmount -= 1 / DashCD * Time.deltaTime;
            if (LeftDashImage.fillAmount <= 0)
            {
                LeftDash = false;
            }
        }
        if (!LeftDash && Input.GetKey(KeyCode.Q))
        {

            GameObject.Instantiate(leftsmoke, transform.position, transform.rotation);
            StartCoroutine(move(DashDuration, true));
            LeftDash = true;
            LeftDashImage.fillAmount = 1;
        }
    }

    //if bool = 1 we move left otherwise right
    private IEnumerator move(float durr, bool direction)
    {
        Vector3 start = transform.position;
        Vector3 end = new Vector3(0, 0, 0);
        if (direction)
        {
            end = transform.position - transform.right * DashDistance;
        }
        else
        {
            end = transform.position + transform.right * DashDistance;
        }
        Vector3 difference = end - start;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / durr)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            x += difference.x * Time.fixedDeltaTime/durr;
            y += difference.y * Time.fixedDeltaTime/durr;
            z += difference.z * Time.fixedDeltaTime/durr;
            transform.position = new Vector3(x, y, z);
            yield return null;
        }
    }
    private void RightUpdate()
    {
        if (RightDash)
        {
            RightDashImage.fillAmount -= 1 / DashCD * Time.deltaTime;
            if (RightDashImage.fillAmount <= 0)
            {
                RightDash = false;
            }
        }
        if (!RightDash && Input.GetKey(KeyCode.E))
        {

            GameObject.Instantiate(rightsmoke, transform.position, transform.rotation);
            StartCoroutine(move(DashDuration, false));
            RightDash = true;
            RightDashImage.fillAmount = 1;
        }
    }

    private void ShieldUpdate()
    {
        if (ShieldOnCD)
        {
            ShieldCD.fillAmount -= 1 / ShieldCooldown * Time.deltaTime;
            if (ShieldCD.fillAmount <= 0)
            {
                ShieldOnCD = false;
            }
        }
        if (!ShieldOnCD && Input.GetKey(KeyCode.R))
        {
            Vector3 position = transform.position;
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            GameObject obj = GameObject.Instantiate(shield, position, rotation);
            ShieldOnCD = true;
            ShieldCD.fillAmount = 1;
        }
    }

    public void inc_cd_shield()
    {
        if (shieldcdupgrade < 5)
        {
            float cost = Mathf.Pow(2, shieldcdupgrade) * 10;
            if (CM.balance >= cost)
            {
                if (shieldcdupgrade < 4)
                {
                    ShieldCooldown -= .25f;
                    CM.removeMoney((int)cost);
                    shieldcdupgrade++;
                }
                else
                {
                    ShieldCooldown -= .5f;
                    CM.removeMoney((int)cost);
                    shieldcdupgrade++;
                }
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
    public void inc_duration_shield()
    {
        if (shielddurationupgrade < 5)
        {
            float cost = Mathf.Pow(2, shielddurationupgrade) * 10;
            if (CM.balance >= cost)
            {
                if (shielddurationupgrade < 4)
                {
                    ShieldUpTime += .25f;
                    CM.removeMoney((int)cost);
                    shielddurationupgrade++;
                }
                else
                {
                    ShieldUpTime += .5f;
                    CM.removeMoney((int)cost);
                    shielddurationupgrade++;
                }
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

    public void inc_cd_dash()
    {
        if (dashcdupgrade < 5)
        {
            float cost = Mathf.Pow(2, dashcdupgrade) * 10;
            if (CM.balance >= cost)
            {
                if (dashcdupgrade < 4)
                {
                    DashCD -= .25f;
                    CM.removeMoney((int)cost);
                    dashcdupgrade++;
                }
                else
                {
                    DashCD -= .5f;
                    CM.removeMoney((int)cost);
                    dashcdupgrade++;
                }
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
    public void inc_distance_dash()
    {
        if (dashdistanceupgrade < 5)
        {
            float cost = Mathf.Pow(2, dashdistanceupgrade) * 10;
            if (CM.balance >= cost)
            {
                if (dashdistanceupgrade < 4)
                {
                    DashDistance += .25f;
                    CM.removeMoney((int)cost);
                    dashdistanceupgrade++;
                }
                else
                {
                    DashDistance += .5f;
                    CM.removeMoney((int)cost);
                    dashdistanceupgrade++;
                }
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
