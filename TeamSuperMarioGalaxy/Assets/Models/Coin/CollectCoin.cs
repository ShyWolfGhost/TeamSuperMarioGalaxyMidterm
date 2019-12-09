using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public GameObject player;
    public AudioClip IAM;
    public AudioSource Source;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
		Source = GameObject.Find("Coin_Sound_Source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerData>().GetCoin();
        
        //coinText = ToString(player.GetComponent<PlayerData>().coins.);
        Debug.Log("Sucesss Coin");
        Source.PlayOneShot(IAM);
        Destroy(gameObject);
        
    }
}
