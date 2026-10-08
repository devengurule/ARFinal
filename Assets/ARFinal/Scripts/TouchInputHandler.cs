using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchInputHandler : MonoBehaviour
{
    [SerializeField] private Camera arCamera;

    public static event Action SpawnPlayer;

    private void OnEnable() => EnhancedTouchSupport.Enable();
    private void OnDisable() => EnhancedTouchSupport.Disable();

    private void Update()
    {
        CheckTouchInput();
    }

    private void CheckTouchInput()
    {
        if (Touch.activeTouches.Count == 0) return;

        Touch touch = Touch.activeTouches[0];


        if(touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            RaycastFromTouch(touch.screenPosition);
        }
    }

    private void RaycastFromTouch(Vector2 screenPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPosition);

        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        if (hit.collider.gameObject.tag == "Start") SpawnPlayer?.Invoke();
    }
}
