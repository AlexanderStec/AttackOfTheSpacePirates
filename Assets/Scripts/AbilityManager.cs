using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public float ShieldCooldown;
    public float ShieldUpTime;

    public GameObject shield;
    public Image ShieldCD;
    public TextMeshProUGUI cantAfford;

    private bool ShieldOnCD;
    private CurrencyManager CM;
    

    private void Awake()
    {
        ShieldOnCD = false;
        CM = this.GetComponent<CurrencyManager>();
    }
    private void FixedUpdate()
    {
        if (ShieldOnCD)
        {
            ShieldCD.fillAmount -= 1 / ShieldCooldown * Time.deltaTime;
            if (ShieldCD.fillAmount <= 0)
            {
                ShieldOnCD = false;
            }
        }
        if (!ShieldOnCD && Input.GetKey(KeyCode.E))
        {
            Vector3 position = transform.position;
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            GameObject obj = GameObject.Instantiate(shield, position, rotation);
            ShieldOnCD = true;
            ShieldCD.fillAmount = 1;
        }
    }
    public void inc_cd()
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
    public void inc_duration()
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
}
