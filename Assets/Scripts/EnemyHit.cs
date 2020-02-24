using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private SpriteRenderer Sr;
    public float changeTime;
    public float numFlashes;
    public Color color;
    private Color startColor;

    private void Start()
    {
        Sr = this.GetComponent<SpriteRenderer>();
        startColor = Sr.material.color;
    }

    public IEnumerator ColorChange(float changeTime)
    {
        for (int i = 0; i < numFlashes * 2; i++)
        {
            if (Sr.material.color == startColor)
            {
                this.GetComponent<SpriteRenderer>().material.SetColor("_Color", color);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().material.SetColor("_Color", startColor);
            }
            yield return new WaitForSeconds(changeTime);
        }
    }
}
