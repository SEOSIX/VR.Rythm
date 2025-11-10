using UnityEngine;
using UnityEngine.InputSystem;

public class Computer : MonoBehaviour
{
    [Header("XR Grip Input (via Action Based Controller)")]
    public InputActionProperty leftGripAction;
    public InputActionProperty rightGripAction;
    
    private void OnEnable()
    {
        leftGripAction.action.Enable();
        rightGripAction.action.Enable();
    }

    private void OnDisable()
    {
        leftGripAction.action.Disable();
        rightGripAction.action.Disable();
    }

    private void Update()
    {
        float leftGrip = leftGripAction.action.ReadValue<float>();
        float rightGrip = rightGripAction.action.ReadValue<float>();

        if (leftGrip > 0.8f)
        {
            Debug.Log("Grip gauche pressé !");
            DebugLogTest();
        }

        if (rightGrip > 0.8f)
        {
            Debug.Log("Grip droit pressé !");
            DebugLogTest();
        }
    }

    public void DebugLogTest()
    {
        Debug.Log("testtesttest");
    }
}