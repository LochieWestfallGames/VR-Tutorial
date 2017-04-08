using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Hand : MonoBehaviour
{
    GameObject heldObject;
    Controller controller;

    public Valve.VR.EVRButtonId pickUpButton;
    public Valve.VR.EVRButtonId dropButton;

    void Start()
    {
        controller = GetComponent<Controller>();
    }

    void Update()
    {
        if (heldObject)
        {
            switch (controller.CurrentTouchPosition())
            {
                case TouchPosition.Up:
                    heldObject.transform.localPosition += Vector3.forward * Time.deltaTime;
                    print("up");
                    break;
                case TouchPosition.Down:
                    heldObject.transform.localPosition -= Vector3.forward * Time.deltaTime;
                    print("down");
                    break;
                case TouchPosition.Left:
                    heldObject.transform.localPosition -= Vector3.right * Time.deltaTime;
                    print("left");
                    break;
                case TouchPosition.Right:
                    heldObject.transform.localPosition += Vector3.right * Time.deltaTime;
                    print("right");
                    break;
                default:
                    print("off");
                    break;
            }
            if ((controller.controller.GetPressUp(pickUpButton) && heldObject.GetComponent<HeldObject>().dropOnRelease) || (controller.controller.GetPressDown(dropButton) && !heldObject.GetComponent<HeldObject>().dropOnRelease))
            {
                heldObject.GetComponent<HeldObject>().Drop();
                heldObject = null;
            }
        }
        else
        {
            if (controller.controller.GetPressDown(pickUpButton))
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);

                foreach (Collider col in cols)
                {
                    if (heldObject == null && col.GetComponent<HeldObject>() && col.GetComponent<HeldObject>().parent == null)
                    {
                        heldObject = col.gameObject;
                        heldObject.GetComponent<HeldObject>().parent = controller;
                        heldObject.GetComponent<HeldObject>().PickUp();
                    }
                }
            }
        }
    }
}
