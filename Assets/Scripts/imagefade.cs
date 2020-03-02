using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imagefade : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer text;
    public float fadespeed;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color newcolor = text.color;
        newcolor.a -= 1 * Time.fixedDeltaTime / fadespeed;
        text.color = newcolor;
    }
}
