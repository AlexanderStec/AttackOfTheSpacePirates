using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float RotSpeed;
    public float ForwardVelocity;
    public float SideVelocity;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.transform.position += transform.up * Time.deltaTime * ForwardVelocity;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.rotation += RotSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.transform.position -= transform.up * Time.deltaTime * ForwardVelocity;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.rotation -= RotSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.transform.position += transform.right * Time.deltaTime * SideVelocity;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.transform.position -= transform.right * Time.deltaTime * SideVelocity;
        }
    }
}
