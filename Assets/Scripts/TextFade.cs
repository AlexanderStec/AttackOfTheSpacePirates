using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    private TextMeshProUGUI text;
    public float fadespeed;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color newcolor = text.color;
        newcolor.a -= 1 * Time.fixedDeltaTime/fadespeed;
        text.color = newcolor;
    }
}
