using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralOrientationHandler : MonoBehaviour
{
    public Vector3 up;
    public float raycastDist;
    public List<Vector3> ups;
    public int directionMemory;

    public Vector3 avgUp()
    {
        Vector3 a = Vector3.zero;
        foreach(Vector3 v in ups)
        {
            a += v;
        }
        a = a / ups.Count;
        return a;
    }

    void Start()
    {
        
    }

    void Update()
    {
        FindUp();
    }

    private void FindUp()
    {
        Ray ray = new Ray();
        if (ups == null)
            ray = new Ray(transform.position, -up * raycastDist);
        else
            ray = new Ray(transform.position, -avgUp() * raycastDist);
        // Debug.DrawRay(r.origin,r.direction);

        Debug.DrawRay(ray.origin, ray.direction * raycastDist);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDist))
        {
            if (hit.collider.tag == "Ground")
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

        if (ups == null || ups.Count < 1)
        {
            ups = new List<Vector3>();
            for (int i = 0; i < directionMemory; i++)
            {
                ups.Add(up);
            }
        }
        else
        {
            ups.RemoveAt(0);
            ups.Add(up);
        }

        Debug.DrawRay(transform.position, avgUp() * 2, Color.magenta);

        Quaternion newR = Quaternion.LookRotation(f, avgUp());
        transform.rotation = newR;
    }
}
