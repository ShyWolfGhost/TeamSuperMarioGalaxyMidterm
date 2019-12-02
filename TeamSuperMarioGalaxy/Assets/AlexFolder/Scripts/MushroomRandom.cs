using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomRandom : MonoBehaviour
{
    //this is the AI part in class
    NavMeshAgent thisAgent;
    public GameObject player;
    [Header("AI")]
    public Transform[] wayPoints;
    public int currentWayPoints = 0;
    public float detectDist = 3f;//Change this to adjust the distance the enemy detects the player
    public float newSpeed = 5f;

    bool detectPlayer = false;//change the way of moving to chase the player

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
            if (thisAgent.remainingDistance < .1f)//number still needs to adjust
            {
                GoToNextPoint();
            }
        }
        else
        {
            thisAgent.destination = player.transform.position;//this should be attack instead of move closer
            thisAgent.speed = newSpeed;
        }
    }

    void GoToNextPoint()
    {
        if (wayPoints.Length == 0)
            return;

        thisAgent.destination = wayPoints[currentWayPoints].position;

        currentWayPoints = Random.Range(0, (wayPoints.Length - 1));
    }
}
