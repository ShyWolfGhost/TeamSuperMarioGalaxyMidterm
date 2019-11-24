using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
    public Vector3 inputVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        inputVector = transform.forward * vertical;
        inputVector += transform.right * horizontal;
        inputVector.Normalize();
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(inputVector.x, GetComponent<Rigidbody>().velocity.y, inputVector.z);
    }
}
