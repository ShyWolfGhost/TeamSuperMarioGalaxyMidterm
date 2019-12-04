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
    public TextMeshProUGUI coinText;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Source = GameObject.Find("AUDIO SOURCE FOR TEST COIN COLLECTION DELETE LATER").GetComponent<AudioSource>();
        coinText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerData>().GetCoin();
        coinText.text = "x " + player.GetComponent<PlayerData>().coins.ToString();
        Debug.Log("Sucesss Coin");
        Source.PlayOneShot(IAM);
        Destroy(gameObject);

    }
}
