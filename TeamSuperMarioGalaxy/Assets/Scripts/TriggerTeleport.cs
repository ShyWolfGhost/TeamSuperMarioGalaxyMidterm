using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{

    public GameObject TeleportDestination;
    public bool boxWorld = false;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.canMove = false;
            playerController.VertMove();
            other.transform.position = TeleportDestination.transform.position;
            //other.transform.eulerAngles = new Vector3(0,0,0);
            if (boxWorld)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.canMove = true;
        }
    }

    // Turn on the bit using an OR operation:
    private void Show()
    {
        Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("BOX");
    }

    // Turn off the bit using an AND operation with the complement of the shifted int:
    private void Hide()
    {
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("BOX"));
    }

}
