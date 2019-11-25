using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlant : MonoBehaviour
{
    public GameObject mainObj, player;
    bool detectPlayer = false;
    public float detectDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mainObj.transform.LookAt(player.transform);

        if (Vector3.Distance(player.transform.position, transform.position) <= detectDist)
            detectPlayer = true;

        if (detectPlayer)//if the enemy doesn't detect the player, it moves between two points
        {
            //this should be attack
        }
    }
}
