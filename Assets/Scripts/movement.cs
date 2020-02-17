using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private float RotSpeed;
    private float ForwardVelocity;
    private float BackwardVelocity;
    StatManager SM;

    // Start is called before the first frame update
    void Start()
    {
        SM = this.gameObject.GetComponent<StatManager>();
        RotSpeed = SM.RotSpeed;
        ForwardVelocity = SM.ForwardVelocity;
        BackwardVelocity = SM.BackwardVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector2 start = transform.position;
            Vector2 end = transform.position + transform.up * ForwardVelocity;
            transform.position = Vector2.Lerp(start, end, Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 start = transform.position;
            Vector2 end = transform.position - transform.up * BackwardVelocity;
            transform.position = Vector2.Lerp(start, end, Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 start = transform.eulerAngles;
            Vector3 end = transform.eulerAngles + new Vector3(0, 0, RotSpeed);
            transform.eulerAngles = Vector3.Lerp(start, end, Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 start = transform.eulerAngles;
            Vector3 end = transform.eulerAngles - new Vector3(0, 0, RotSpeed);
            transform.eulerAngles = Vector3.Lerp(start, end, Time.fixedDeltaTime);
        }
    }
}
