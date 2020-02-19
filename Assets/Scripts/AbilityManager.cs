using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public float ShieldCooldown;
    public float ShieldUpTime;

    public GameObject shield;
    public Image ShieldCD;

    private bool ShieldOnCD;
    

    private void Awake()
    {
        ShieldOnCD = false;
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
}
