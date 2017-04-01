using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class HeldObject : MonoBehaviour
{
    [HideInInspector]
    public Controller parent;

    public bool dropOnRelease;

    public UnityEvent pickUp;

    public UnityEvent drop;

    public void PickUp ()
    {
        pickUp.Invoke();
        if (pickUp.GetPersistentEventCount() == 0)
        {
            DefaultPickUp();
        }
    }

    public void Drop ()
    {
        drop.Invoke();
        if (drop.GetPersistentEventCount() == 0)
        {
            DefaultDrop();
        }
        parent = null;
    }

    public void DefaultDrop ()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = parent.GetComponent<Hand>().simulator.velocity;
        parent = null;
    }

    public void DefaultPickUp ()
    {
        transform.parent = parent.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
