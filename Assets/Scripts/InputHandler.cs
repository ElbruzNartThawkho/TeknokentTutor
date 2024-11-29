using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    Movement movement;
    private void Awake()
    {
        movement = GetComponent<Movement>();
    }
    public void Move_performed(InputAction.CallbackContext obj)
    {
        movement.horizontal = obj.ReadValue<Vector2>().x;
        movement.vertical = obj.ReadValue<Vector2>().y;
    }
    public void Jump_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed) movement.JumpPerformed();
    }
    public void Run_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed) movement.RunPerformed();
        if (obj.canceled) movement.RunCancled();
    }
}
