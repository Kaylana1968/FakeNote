using UnityEngine;

public class ClickOnNote : MonoBehaviour
{
    private InputSystem_Actions inputs;

    private void Awake()
    {
        inputs = new InputSystem_Actions();

        inputs.Keyboard.A.performed += context =>
        {
            Debug.Log("A a été pressé");
        };
    }

    private void OnEnable()
    {
        inputs.Keyboard.Enable();
    }

    private void OnDisable()
    {
        inputs.Keyboard.Disable();
    }
}
