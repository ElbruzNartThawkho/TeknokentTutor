using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    Movement movement;
    private void Awake()
    {
        movement = GetComponent<Movement>();
    }
    public void Move(InputAction.CallbackContext callbackContext)
    {
        movement.Move(callbackContext.ReadValue<Vector2>().x, callbackContext.ReadValue<Vector2>().y);
    }
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            movement.Jump();
        }
    }
}
