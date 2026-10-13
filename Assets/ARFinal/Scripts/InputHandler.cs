using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.EventSystems;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputHandler : MonoBehaviour
{
    public static event Action<Vector2> moveChange;

    private int movementFingerID = -1;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        movementFingerID = -1;
        moveChange?.Invoke(Vector2.zero);
    }

    private void Update()
    {
        Vector2 movement = Vector2.zero;
        bool foundMovementFinger = false;

        foreach (var touch in Touch.activeTouches)
        {
            if(touch.phase == TouchPhase.Began && movementFingerID == -1)
            {
                if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(touch.touchId))
                {
                    continue;
                }

                movementFingerID = touch.touchId;
            }

            if (touch.touchId != movementFingerID) continue;

            foundMovementFinger = true;

            if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                movementFingerID = -1;
                movement = Vector2.zero;
            }
            else
            {
                movement = touch.delta;
            }

            break;
        }

        if (!foundMovementFinger)
        {
            movementFingerID = -1;
        }

        moveChange?.Invoke(movement);
    }
}
