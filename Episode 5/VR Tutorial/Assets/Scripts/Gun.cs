using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeldObject))]
public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public float power;
    public Transform firepoint;

    public Valve.VR.EVRButtonId shootButton;

    public bool automatic;
    public float cooldownTime;
    float time;

    HeldObject heldObject;

    void Start()
    {
        heldObject = GetComponent<HeldObject>();
    }

    void Update()
    {
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (heldObject.parent != null && ((heldObject.parent.controller.GetPressDown(shootButton) && !automatic) || (heldObject.parent.controller.GetPress(shootButton) && automatic)))
            {
                time = cooldownTime;
                GameObject proj = (GameObject)Instantiate(projectile, firepoint.position, firepoint.rotation);
                proj.GetComponent<Rigidbody>().velocity = firepoint.forward * power;
            }
        }
    }
}
