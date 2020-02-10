using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float FireCooldown = 1f;
    private float lastfire;
    public GameObject prefab;
    public float SpawnHeight;
    // Start is called before the first frame update
    public void Fire()
    {
        float time = Time.time;
        if (time > lastfire + FireCooldown)
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
