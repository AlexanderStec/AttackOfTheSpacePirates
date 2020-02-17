using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public float ShieldCooldown;
    public float ShieldUpTime;
    public GameObject shield;
    private float Ctime = 0;
    private float lastLogTime;

    private void Awake()
    {
        Ctime = -ShieldCooldown;
        lastLogTime = -ShieldCooldown;
    }
    private void FixedUpdate()
    {
        if (Time.time >= Ctime + ShieldCooldown && Time.time > lastLogTime + ShieldCooldown)
        {
            lastLogTime = Time.time;
            Debug.Log("Shields Available");
        }
        if (Time.time >= Ctime + ShieldCooldown && Input.GetKey(KeyCode.Z))
        {
            Ctime = Time.time;
            Vector3 position = transform.position;
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            GameObject obj = GameObject.Instantiate(shield, position, rotation);
        }
    }
}
