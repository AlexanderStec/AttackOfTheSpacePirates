using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private SpriteRenderer Sr;
    private StatManager SM;
    public float changeTime;
    public float numFlashes;
    public Color color;

    void Start()
    {
        Sr = this.GetComponent<SpriteRenderer>();
        SM = this.GetComponent<StatManager>();
    }
       
    public IEnumerator ColorChange(float changeTime)
    {
        for (int i = 0; i < numFlashes * 2; i++)
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
