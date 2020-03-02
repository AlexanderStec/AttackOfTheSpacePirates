using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private bool ShieldOnCD;
    private bool LeftDash;
    private bool RightDash;
    private CurrencyManager CM;
    

    private void Awake()
    {
        ShieldOnCD = false;
        LeftDash = false;
        RightDash = false;
        CM = this.GetComponent<CurrencyManager>();
    }
    private void FixedUpdate()
    {
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
        if (CM.balance >= 1)
        {
            ShieldCooldown -= 1f;
            CM.balance--;
        }
        else
        {
            Color newColor = cantAfford.color;
            newColor.a = 1;
            cantAfford.color = newColor;
        }
    }
    public void inc_duration_shield()
    {
        if (CM.balance >= 1)
        {
            ShieldUpTime++;
            CM.balance--;
        }
        else
        {
            Color newColor = cantAfford.color;
            newColor.a = 1;
            cantAfford.color = newColor;
        }
    }

    public void inc_cd_dash()
    {
        if (CM.balance >= 1)
        {
            DashCD -= .5f;
            CM.balance--;
        }
        else
        {
            Color newColor = cantAfford.color;
            newColor.a = 1;
            cantAfford.color = newColor;
        }
    }
    public void inc_distance_dash()
    {
        if (CM.balance >= 1)
        {
            DashDistance+=.5f;
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
