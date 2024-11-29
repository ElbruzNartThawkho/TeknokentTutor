using UnityEngine;

public class Movement : MonoBehaviour
{
    public LayerMask groundCheck;
    public float walkSpeed;
    public float runSpeed;
    public float jumpPower;
    //public float dashSpeed;
    //public float dashTime;
    //public float slideSpeed;
    //public float slideTime;
    public float smoothFactor;
    public float gravity;

    [HideInInspector] public float vertical, horizontal;
    float velocityHorizontal;
    float speed;

    //bool doubleJump = false;
    //bool dash = true;
    //bool isSliding = false;

    Vector3 velocity = Vector3.zero;

    CharacterController charactercontroller;
    Animator animator;

    private void OnDrawGizmos()
    {
        if (GroundCheck()) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), .5f);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        charactercontroller = GetComponent<CharacterController>();
        speed = walkSpeed;
    }
    private void FixedUpdate()
    {
        MovementAndAnim(horizontal, vertical);
    }
    public void MovementAndAnim(float h, float v)
    {
        if (GroundCheck())
        {
            animator.SetBool("Grounded", true);
            //dash = true;
        }
        else
        {
            animator.SetBool("Grounded", false);
            velocityHorizontal -= gravity;
        }
        //if (!isSliding)
        //{
        //    if (AbovedCheck())
        //    {
        //        charactercontroller.height = 1;
        //        charactercontroller.center = new Vector3(charactercontroller.center.x, -.5f, charactercontroller.center.z);
        //        transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x, 0, transform.GetChild(0).localPosition.z);
        //    }
        //    else
        //    {
        //        charactercontroller.height = 2;
        //        charactercontroller.center = new Vector3(charactercontroller.center.x, 0, charactercontroller.center.z);
        //        transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x, .5f, transform.GetChild(0).localPosition.z);
        //    }
        //}
        velocity = Vector3.Lerp(velocity, (h * speed * transform.right + v * speed * transform.forward), smoothFactor * Time.deltaTime);
        charactercontroller.Move(Time.deltaTime * velocity + Time.deltaTime * velocityHorizontal * Vector3.up);
        if (speed == walkSpeed)
        {
            animator.SetFloat("Horizontal", h);
            animator.SetFloat("Vertical", v);
        }
        else
        {
            animator.SetFloat("Horizontal", h * 2);
            animator.SetFloat("Vertical", v * 2);
        }

        Quaternion look = Camera.main.transform.rotation;
        look.x = 0;
        look.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, look, smoothFactor * Time.deltaTime);
    }
    public void JumpPerformed()
    {
        if (GroundCheck())
        {
            //doubleJump = true;
            velocityHorizontal = 0;
            velocityHorizontal += jumpPower;
        }
        //else if (doubleJump)
        //{
        //    velocityHorizontal = 0;
        //    velocityHorizontal += jumpPower;
        //    doubleJump = false;
        //}
    }
    public void RunPerformed()
    {
        if (GroundCheck())
        {
            speed = runSpeed;
        }
        //else if (dash)
        //{
        //    StartCoroutine(Dash());
        //    dash = false;
        //}
    }
    public void RunCancled()
    {
        speed = walkSpeed;
    }
    //public void SlidePerformed()
    //{
    //    if (GroundCheck() && !isSliding)
    //    {
    //        StartCoroutine(Slide());
    //        dash = false;
    //    }
    //}
    //IEnumerator Dash()
    //{
    //    Vector3 dashDir = (horizontal > 0.1f || vertical > 0.1f || horizontal < -0.1f || vertical < -0.1f) ? horizontal * transform.right + vertical * transform.forward : transform.forward;
    //    dashDir.y = 0;
    //    float startTime = Time.time;
    //    while (Time.time < startTime + dashTime)
    //    {
    //        charactercontroller.Move(dashSpeed * Time.fixedDeltaTime * dashDir);
    //        yield return new WaitForFixedUpdate();
    //    }
    //    yield break;
    //}
    //IEnumerator Slide()
    //{
    //    if (vertical > 0.3f)
    //    {
    //        isSliding = true;
    //        Vector3 slideDir = transform.forward;
    //        slideDir.y = 0;
    //        float startTime = Time.time;
    //        charactercontroller.height = 1;
    //        charactercontroller.center = new Vector3(charactercontroller.center.x, -.5f, charactercontroller.center.z);
    //        transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x, 0, transform.GetChild(0).localPosition.z);
    //        while (Time.time < startTime + slideTime)
    //        {
    //            charactercontroller.Move(slideSpeed * Time.fixedDeltaTime * slideDir);
    //            yield return new WaitForFixedUpdate();
    //        }
    //        if (!AbovedCheck())
    //        {
    //            charactercontroller.height = 2;
    //            charactercontroller.center = new Vector3(charactercontroller.center.x, 0, charactercontroller.center.z);
    //            transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x, .5f, transform.GetChild(0).localPosition.z);
    //        }
    //        isSliding = false;
    //        yield break;
    //    }
    //    else yield break;
    //}
    bool GroundCheck() => Physics.CheckSphere(transform.position, .5f, groundCheck, QueryTriggerInteraction.Ignore);
    //bool AbovedCheck() => Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 1.2f, groundCheck);

}
