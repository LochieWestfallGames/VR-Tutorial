using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
        }
    }

    public TouchPosition CurrentTouchPosition ()
    {
        Vector2 pos = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        bool isTop = pos.y >= 0;
        bool isRight = pos.x >= 0;

        if (isTop)
        {
            if (isRight)
            {
                if (pos.y > pos.x)
                {
                    return TouchPosition.Up;
                }
                else if (pos.y < pos.x)
                {
                    return TouchPosition.Right;
                }
            }
            else
            {
                if (pos.y > -pos.x)
                {
                    return TouchPosition.Up;
                }
                else if (pos.y < -pos.x)
                {
                    return TouchPosition.Left;
                }
            }
        }
        else
        {
            if (isRight)
            {
                if (-pos.y > pos.x)
                {
                    return TouchPosition.Down;
                }
                else if (-pos.y < pos.x)
                {
                    return TouchPosition.Right;
                }
            }
            else
            {
                if (-pos.y > -pos.x)
                {
                    return TouchPosition.Down;
                }
                else if (-pos.y < -pos.x)
                {
                    return TouchPosition.Left;
                }
            }
        }

        return TouchPosition.Off;
    }
}

public enum TouchPosition
{
    Off, Up, Down, Left, Right
}