using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomOne : MonoBehaviour
{
    //this is the AI part in class
    NavMeshAgent thisAgent;
    public GameObject player;
    [Header("GetKilled")]
    public float detectBeJumpedDist;
    [Header("AI")]
    public Transform[] wayPoints;
    public int currentWayPoints = 0;
    public float detectDist = 3f;//Change this to adjust the distance the enemy detects the player

    bool detectPlayer = false;//change the way of moving to chase the player

    private float waitTimer;
    public float waitTime;

    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        GoToNextPoint();
    }

    void Update()
    {
        Debug.Log(waitTimer);

            detectPlayer = Vector3.Distance(player.transform.position, transform.position) <= detectDist;

        if (!detectPlayer)//if the enemy doesn't detect the player, it moves between two points
        {
            thisAgent.isStopped = false;
            if (thisAgent.remainingDistance < .1f)//number still needs to adjust
            {
                GoToNextPoint();
            }
        }
        else
        {
            if (waitTimer <= 0f)
            {
                thisAgent.isStopped = false;
                thisAgent.destination = player.transform.position;//this should be attack instead of move closer
            }
            else
            {
                thisAgent.isStopped = true;
            }
        }
        waitTimer -= Time.deltaTime;
    }

    void GoToNextPoint()
    {
        if (wayPoints.Length == 0)
            return;

        thisAgent.destination = wayPoints[currentWayPoints].position;

        currentWayPoints = currentWayPoints + 1;

        if (currentWayPoints >= wayPoints.Length)
        {
            currentWayPoints = 0;
        }
    }

    public void Wait()
    {
        waitTimer = waitTime;
    }
}