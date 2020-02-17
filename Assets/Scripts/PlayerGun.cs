using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private float lastfire;
    public GameObject prefab;
    private float SpawnHeight;
    StatManager SM;

    // Start is called before the first frame update
    private void Start()
    {
        SM = this.gameObject.GetComponent<StatManager>();
        SpawnHeight = SM.forward_projectile_spawn_height;
    }

    public void Fire()
    {
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
        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }
}
