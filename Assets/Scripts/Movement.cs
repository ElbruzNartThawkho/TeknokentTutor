using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2, gravity = 0.8f, jumpPower;
    CharacterController characterController;
    Vector3 velocity;
    Quaternion rotation;
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        characterController.Move(moveSpeed * velocity * Time.deltaTime);

        rotation = Camera.main.transform.rotation;
        rotation.x = 0; rotation.z = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);

        if (!characterController.isGrounded)
        {
            velocity.y -= gravity;
        }
    }
    public void Move(float x, float y)
    {
        velocity = transform.forward * y + transform.right * x;
        animator.SetFloat("Horizontal", y);
        animator.SetFloat("Vertical", x);
    }
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = 0;
            velocity.y += jumpPower;
        }
    }
}
