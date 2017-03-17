using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeldObject : MonoBehaviour
{
    [HideInInspector]
    public Controller parent;
}
