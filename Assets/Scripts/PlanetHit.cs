﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHit : MonoBehaviour
{
    private SpriteRenderer Sr;
    public float changeTime;
    public float numFlashes;
    public Color color;

    void Start()
    {
        Sr = this.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("SimpleEnemy")|| other.gameObject.CompareTag("Broad"))
        {
            StartCoroutine(ColorChange(changeTime));
        }
    }

    private IEnumerator ColorChange(float changeTime)
    {
        for(int i = 0; i < numFlashes * 2; i++)
        {
            if (Sr.material.color == Color.white)
            {
                this.GetComponent<SpriteRenderer>().material.SetColor("_Color", color);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
            }
            yield return new WaitForSeconds(changeTime);
        }
    }
}
