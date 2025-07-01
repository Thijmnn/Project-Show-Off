using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class LoadingScreenCountdown : MonoBehaviour
{
    public sceneFader sceneFader;

    private void OnEnable()
    {
        // Subscribe to *all* button events on the current gamepad
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, InputDevice device)
    {
        if (!(device is Gamepad gamepad))
            return;

        foreach (var control in gamepad.allControls)
        {
            if (control is ButtonControl button && button.wasPressedThisFrame)
            {
                sceneFader.TriggerFadeOut();
                // Optionally disable listening after the first button
                InputSystem.onEvent -= OnInputEvent;
                break;
            }
        }
    }
}