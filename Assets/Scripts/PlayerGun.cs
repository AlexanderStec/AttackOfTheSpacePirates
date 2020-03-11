using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private float lastfire;
    public GameObject prefab;
    private float SpawnHeight;
    StatManager SM;
    public GameObject Reload;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public GameObject spawn6;
    public GameObject spawn7;


    // Start is called before the first frame update
    private void Start()
    {
        SM = this.gameObject.GetComponent<StatManager>();
        lastfire = -SM.firing_rate;
        Reload.gameObject.transform.localScale += new Vector3(1, 0, 0);
        SpawnHeight = SM.forward_projectile_spawn_height;
    }

    public void Fire()
    {
        if (Reload.gameObject.transform.localScale.x >= 1)
        {
            Reload.gameObject.transform.localScale -= new Vector3(1, 0, 0);
        }
        float time = Time.time;
        if (time > lastfire + SM.firing_rate)
        {
            lastfire = time;
            Vector3 position = transform.position + transform.up * SpawnHeight;
            Quaternion rotation = transform.rotation;
            if (SM.num_bullet == 1)
            {
                GameObject.Instantiate(prefab, spawn1.transform.position , rotation);
            }
            if (SM.num_bullet == 2)
            {
                GameObject.Instantiate(prefab, spawn2.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn3.transform.position, rotation);
            }
            if (SM.num_bullet == 3)
            {
                GameObject.Instantiate(prefab, spawn2.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn3.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn1.transform.position, rotation);
            }
            if (SM.num_bullet == 4)
            {
                GameObject.Instantiate(prefab, spawn2.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn3.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn4.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn5.transform.position, rotation);
            }
            if (SM.num_bullet == 5)
            {
                GameObject.Instantiate(prefab, spawn1.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn2.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn3.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn4.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn5.transform.position, rotation);
            }
            if (SM.num_bullet == 6)
            {
                GameObject.Instantiate(prefab, spawn2.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn3.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn4.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn5.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn6.transform.position, rotation);
                GameObject.Instantiate(prefab, spawn7.transform.position, rotation);
            }
        }
    }

    void FixedUpdate()
    {
        if (Reload.gameObject.transform.localScale.x < 1)
        {
            Reload.gameObject.transform.localScale += new Vector3((Time.fixedDeltaTime/SM.firing_rate), 0, 0);
        }
        if (Reload.gameObject.transform.localScale.x >= 1)
        {
            Reload.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }
}
