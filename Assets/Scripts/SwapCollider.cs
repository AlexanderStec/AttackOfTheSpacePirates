using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCollider : MonoBehaviour
{
    private CircleCollider2D CC;
    private PolygonCollider2D PC;
    // Start is called before the first frame update
    void Start()
    {
        CC = this.GetComponent<CircleCollider2D>();
        PC = this.GetComponent<PolygonCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Wall"))
        {
            CC.enabled = true;
            PC.enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Wall"))
        {
            CC.enabled = false;
            PC.enabled = true;
        }
    }
}
