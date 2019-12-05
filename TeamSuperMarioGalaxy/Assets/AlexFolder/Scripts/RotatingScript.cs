using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingScript : MonoBehaviour
{
    [Header("Down Raycasting")]
    public Transform motherObj;
    public Vector3 raycastOriginOffset;
    public float raycastDist;
    public Vector3 up;
    [Header("Gravity")]
    public float fallTime;
    public float timeToMaxFall;
    public float maxFallSpeed;
    public AnimationCurve fallCurve;
    public float gravityCheckDist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        FindUp();
    }

    private void FindUp()
    {
        Ray ray = new Ray(transform.position, -up * raycastDist);
        // Debug.DrawRay(r.origin,r.direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDist))
        {
            up = hit.normal;
        }

        up = up.normalized;

        Vector3 r = transform.TransformVector(Vector3.right);

        r = r.normalized;

        Vector3 f = Vector3.Cross(r, up);

        f = f.normalized;

        Debug.DrawRay(transform.position, f, Color.blue);
        Debug.DrawRay(transform.position, up, Color.green);
        Debug.DrawRay(transform.position, r, Color.red);

        transform.rotation = Quaternion.LookRotation(f, up);
    }
}
