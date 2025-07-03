using UnityEngine;
using UnityEngine.InputSystem;

public class LoadingScreenCountdown : MonoBehaviour
{
    public sceneFader sceneFader;
    private InputAction anyButtonAction;

    private void Awake()
    {
        // Bind a "catch all" button press
        anyButtonAction = new InputAction(type: InputActionType.Button, binding: "<Gamepad>/buttonSouth");
        anyButtonAction.AddBinding("<Gamepad>/buttonNorth");
        anyButtonAction.AddBinding("<Gamepad>/buttonWest");
        anyButtonAction.AddBinding("<Gamepad>/buttonEast");
        anyButtonAction.AddBinding("<Gamepad>/start");
        anyButtonAction.AddBinding("<Gamepad>/select");
        anyButtonAction.AddBinding("<Gamepad>/dpad");
        anyButtonAction.AddBinding("<Gamepad>/leftShoulder");
        anyButtonAction.AddBinding("<Gamepad>/rightShoulder");
        anyButtonAction.AddBinding("<Gamepad>/leftTrigger");
        anyButtonAction.AddBinding("<Gamepad>/rightTrigger");

        anyButtonAction.performed += ctx => OnAnyButtonPressed();
    }

    private void OnEnable()
    {
        anyButtonAction.Enable();
    }

    private void OnDisable()
    {
        anyButtonAction.Disable();
    }

    private void OnAnyButtonPressed()
    {
        sceneFader.TriggerFadeOut();
        anyButtonAction.Disable(); // optional to prevent repeat
    }
}