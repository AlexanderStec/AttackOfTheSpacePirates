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
            GameObject.Instantiate(prefab, position, rotation);
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
