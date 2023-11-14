using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupZombieController : ZombieController
{

    public float Distance = 2f;
    override protected InputInfo GetInput()
    {
        InputInfo input = new InputInfo();

        Vector3 totalForce = Vector3.zero;

        foreach (var collider in Physics.OverlapSphere(transform.position, 20f))
        {
            if (collider.tag == "Zombie" && collider.gameObject != gameObject)
            {
                Vector3 r = transform.InverseTransformPoint(collider.transform.position);
                float currentDistance = r.magnitude;
                float forceMagnitude = 1f / Distance - 1f / currentDistance;
                totalForce += forceMagnitude / currentDistance * r;
            }
        }
        if (totalForce.x > 0)
            input.right = true;
        else
            input.left = true;
        if (totalForce.z > 0.1f)
            input.forward = true;

        return input;

    }
}
