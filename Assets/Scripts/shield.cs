using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    private float Ctime;
    private AbilityManager AM;
    private StatManager SM;
    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Ctime = Time.time;
        AM = player.GetComponentInParent<AbilityManager>();
        SM = player.GetComponentInParent<StatManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position;
        if(Time.time > (Ctime + AM.ShieldUpTime))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("SimpleEnemy"))
        {
            other.gameObject.GetComponent<StatManager>().take_damage(SM.Crash_Damage);
        }
        if (other.gameObject.tag.Equals("Broad"))
        {
            other.gameObject.GetComponent<StatManager>().take_damage(SM.Crash_Damage);
        }
        if (other.gameObject.tag.Equals("EP"))
        {
            Destroy(other.gameObject);
        }
    }
}
