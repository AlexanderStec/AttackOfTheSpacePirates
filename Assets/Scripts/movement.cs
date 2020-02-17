using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float RotSpeed;
    public float ForwardVelocity;
    public float BackwardVelocity;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector2 start = rb.transform.position;
            Vector2 end = rb.transform.position + transform.up * ForwardVelocity;
            rb.transform.position = Vector2.Lerp(start, end, Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 start = rb.transform.position;
            Vector2 end = rb.transform.position - transform.up * BackwardVelocity;
            rb.transform.position = Vector2.Lerp(start, end, Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 start = rb.transform.eulerAngles;
            Vector3 end = rb.transform.eulerAngles + new Vector3(0, 0, RotSpeed);
            rb.transform.eulerAngles = Vector3.Lerp(start, end, Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 start = rb.transform.eulerAngles;
            Vector3 end = rb.transform.eulerAngles - new Vector3(0, 0, RotSpeed);
            rb.transform.eulerAngles = Vector3.Lerp(start, end, Time.fixedDeltaTime);
        }
    }
}
