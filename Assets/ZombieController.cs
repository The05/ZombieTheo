using UnityEngine;
using UnityEngine.UIElements;

public abstract class ZombieController : MonoBehaviour
{
    protected class InputInfo { public bool forward = false, right = false, left = false, jump = false, run = false; };

    public float WalkingSpeed = 1f;
    public float RunningSpeed = 10f;
    public float TurningSpeed = 150f;
    public float JumpSpeed = 5f;
    public float WalkingFriction = 3f;
    public float TurningFriction = 3f;

    CharacterController characterController;
    Animator animator;
    Vector3 velocity = Vector3.zero;
    float angularVelocity = 0f;
    bool fallen = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    protected abstract InputInfo GetInput();
    

    void Update()
    {
        InputInfo input = GetInput();

        if (Input.GetKeyDown(KeyCode.S))
        {
            fallen = !fallen;
        };

        if (characterController.isGrounded)
        {
            float newVelocity = 0f;
            float newAngularVelocity = 0f;

            if (!fallen)
            {
                if (input.forward)
                {
                    if (input.run)
                        newVelocity += RunningSpeed;
                    else
                        newVelocity += WalkingSpeed;
                }
                if (input.right )
                    newAngularVelocity += TurningSpeed;
                if (input.left )
                    newAngularVelocity -= TurningSpeed;
                if (input.jump)
                    velocity.y = JumpSpeed;
            }

            velocity.z = Mathf.Lerp(velocity.z, newVelocity, WalkingFriction * Time.deltaTime);
            angularVelocity = Mathf.Lerp(angularVelocity, newAngularVelocity, TurningFriction * Time.deltaTime);
        }
        if (!characterController.isGrounded)
        {
            velocity.y -= 9.82f * Time.deltaTime;
        }

        animator.SetFloat("speed", velocity.z);
        animator.SetBool("Fallen", fallen);
        transform.Rotate(Vector3.up, angularVelocity * Time.deltaTime);
        characterController.Move(transform.TransformVector(velocity) * Time.deltaTime);

    }

}
