using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookForSign : MonoBehaviour
{
    public Image enterPrompt;
    public GameObject message;

    private bool messageUp;

    void Start()
    {
        message.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(messageUp)
            {
                messageUp = false;
                message.SetActive(false);
                GetComponent<PlayerController>().state = PlayerController.State.NORMAL;
            }
            else
            {
                messageUp = true;
                message.SetActive(true);
                GetComponent<PlayerController>().state = PlayerController.State.DISABLED;
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 4f))
        {
            if (hit.collider.tag == "Signpost" && GetComponent<PlayerController>().state != PlayerController.State.DISABLED)
            {
                enterPrompt.gameObject.SetActive(true);
            }
            else
                enterPrompt.gameObject.SetActive(false);
        }
        else
            enterPrompt.gameObject.SetActive(false);
    }
}
