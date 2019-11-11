using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomOne : MonoBehaviour
{
    //this is the AI part in class
    NavMeshAgent thisAgent;
    public GameObject player;
    [Header("AI")]
    public Transform[] wayPoints;
    public int currentWayPoints = 0;
    public float detectDist = 3f;//Change this to adjust the distance the enemy detects the player

    public static MushroomOne me;//just in case you have to use it

    bool detectPlayer = false;//change the way of moving to chase the player

    private void Awake()
    {
        me = this;
    }

    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        GoToNextPoint();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= detectDist)
            detectPlayer = true;

        if (!detectPlayer)//if the enemy doesn't detect the player, it moves between two points
        {
            if (thisAgent.remainingDistance < .1f)
            {
                GoToNextPoint();
            }
        }
        else
        {
            thisAgent.destination = player.transform.position;
        }


    }

    void GoToNextPoint()
    {
        if (wayPoints.Length == 0)
            return;

        thisAgent.destination = wayPoints[currentWayPoints].position;

        currentWayPoints = currentWayPoints + 1;
        
        if(currentWayPoints >= wayPoints.Length)
        {
            currentWayPoints = 0;
        }
    }
}
