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
        player.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerData>().GetCoin();
        coinText.text = player.GetComponent<PlayerData>().coins.ToString();
        
        //coinText = ToString(player.GetComponent<PlayerData>().coins.);
        Debug.Log("Sucesss Coin");
        Source.PlayOneShot(IAM);
        Destroy(gameObject);
        
    }
}
