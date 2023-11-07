using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float WalkingSpeed = 1f;
    public float RunningSpeed = 10f;
    public float TurningSpeed = 150f;
    public float JumpSpeed = 5f;
    public float WalkingFriction = 3f;
    public float TurningFriction = 3f;

    Animator animator;

    CharacterController characterController;
    Vector3 velocity = Vector3.zero;
    float angularVelocity = 0f;
    bool fallen = false;


    void Start()
    {
        // Find our character controller once and save it
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (characterController.isGrounded && !fallen)
        {
            // If grounded: Find the users desired walking velocity and angular
            // (turning) velocity. Also see if user wants to jump.
            float newVelocity = 0f;
            float newAngularVelocity = 0f;

            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    newVelocity += RunningSpeed;
                else
                    newVelocity += WalkingSpeed;
            }
            if (Input.GetKey(KeyCode.D))
                newAngularVelocity += TurningSpeed;
            if (Input.GetKey(KeyCode.A))
                newAngularVelocity -= TurningSpeed;
            if (Input.GetKey(KeyCode.Space))
                velocity.y = JumpSpeed;

            velocity.z = Mathf.Lerp(velocity.z, newVelocity, WalkingFriction * Time.deltaTime);
            angularVelocity = Mathf.Lerp(angularVelocity, newAngularVelocity, TurningFriction * Time.deltaTime);
        }
        if(!characterController.isGrounded)
        {
            velocity.y -= 9.82f * Time.deltaTime;
        }

        // change = velocity * delta time
        transform.Rotate(Vector3.up, angularVelocity * Time.deltaTime);
        characterController.Move(transform.TransformVector(velocity) * Time.deltaTime);

        animator.SetFloat("speed", velocity.z);
    }

}
