using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statKeeper : MonoBehaviour
{
    private StatManager stats;
    private AbilityManager abilities;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            stats = player.GetComponent<StatManager>();
            abilities = player.GetComponent<AbilityManager>();
        }
    }
}
