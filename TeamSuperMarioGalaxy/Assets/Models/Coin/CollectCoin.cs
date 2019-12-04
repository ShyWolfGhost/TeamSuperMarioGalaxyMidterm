using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public GameObject player;
    public AudioClip IAM;
    public AudioSource Source;



    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerData>().coins=player.GetComponent<PlayerData>().coins+1;
        Debug.Log("Sucesss Coin");
        Source.PlayOneShot(IAM);
        
    }
}
