using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeldObject))]
public class SlidingDrawer : MonoBehaviour {

    Transform parent;
    public Transform pointA;
    public Transform pointB;

    Vector3 offset;

    HeldObject heldObject;

	void Start () {
        heldObject = GetComponent<HeldObject>();
	}

	void Update ()
    {
		if (parent != null)
        {
            transform.position = ClosestPointOnLine(parent.position) - offset;
        }
	}

    public void PickUp ()
    {
        parent = heldObject.parent.transform;

        offset = parent.position - transform.position;
    }

    public void Drop ()
    {
        heldObject.simulator.transform.position = transform.position + offset;

        parent = heldObject.simulator.transform;
    }

    Vector3 ClosestPointOnLine (Vector3 point)
    {
        Vector3 va = pointA.position + offset;
        Vector3 vb = pointB.position + offset;

        Vector3 vVector1 = point - va;

        Vector3 vVector2 = (vb - va).normalized;

        float t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return va;

        if (t >= Vector3.Distance(va, vb))
            return vb;

        Vector3 vVector3 = vVector2 * t;

        Vector3 vClosestPoint = va + vVector3;

        return vClosestPoint;
    }
}
