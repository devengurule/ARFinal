using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class JumpButton : MonoBehaviour
{
    public static event Action OnJump;
    private RectTransform buttonRect;
    private Canvas canvas;
    private Image buttonImage;
    private Color startColor;
    private int buttonTouchId = -1;

    private void Awake()
    {
        buttonRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        buttonImage = GetComponent<Image>();
        startColor = buttonImage.color;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if (canvas == null || buttonRect == null) return;

        foreach (var touch in Touch.activeTouches)
        {
            if (touch.touchId == buttonTouchId)
            {
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) buttonImage.color = startColor;
                else continue;
                buttonTouchId = -1;
            }
            else
            {
                if (touch.phase != TouchPhase.Began) continue;

                Camera uiCamera = canvas.rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.rootCanvas.worldCamera;

                bool insideButton = RectTransformUtility.RectangleContainsScreenPoint(buttonRect, touch.screenPosition, uiCamera);

                if (!insideButton) continue;

                if (buttonTouchId == -1) buttonTouchId = touch.touchId;
                
                buttonImage.color = Color.red;

                OnJump?.Invoke();
            }
        }

    }
}