using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statKeeper : MonoBehaviour
{
    public StatManager startingStats;
    public StatManager currentStats;
    public AbilityManager startingAbilities;
    public AbilityManager CurrentAbilities;
    private GameObject player;
    private AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        am = FindObjectOfType<AudioManager>();
        am.Play("MainTheme");
        startingAbilities = player.GetComponent<AbilityManager>();
        startingStats = player.GetComponent<StatManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            CurrentAbilities = player.GetComponent<AbilityManager>();
            currentStats = player.GetComponent<StatManager>();
        }
    }
}
