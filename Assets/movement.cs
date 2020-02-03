using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float RotSpeed;
    public float Acceleration;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(rb.transform.up * Acceleration);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-RotSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(RotSpeed);
        }
    }
}
