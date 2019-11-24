using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlant : MonoBehaviour
{
    public Transform Player;//Assign player to this transform so that the plant can look at the player.

    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(new Vector3(Player.position.x, transform.position.y, Player.position.z), Vector3.left);
    }
}
