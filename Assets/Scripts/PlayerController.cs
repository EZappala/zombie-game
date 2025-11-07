using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerController : EntityController
{
    [SerializeField]
    private float player_speed = 5;

    [SerializeField]
    private float jump_height = 1.5f;

    [SerializeField]
    private float gravity_value = -9.81f;

    private CharacterController character_controller;
    private CinemachineCamera cine_cam;
    private Vector3 velocity;
    private bool grounded;

    private InputAction move;
    private InputAction jump;

    private void OnValidate()
    {
        cine_cam = GameObject.FindFirstObjectByType<CinemachineCamera>();
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        character_controller = GetComponent<CharacterController>();

        move = InputSystem.actions.FindAction("Move", true);
        jump = InputSystem.actions.FindAction("Jump", true);
    }

    private void OnEnable()
    {
        move?.Enable();
        jump?.Enable();
    }

    private void OnDisable()
    {
        move?.Disable();
        jump?.Disable();
    }

    private void Update()
    {
        use_move();
    }

    private void use_move()
    {
        grounded = character_controller.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        Vector2 input = move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0, input.y);
        movement = Vector3.ClampMagnitude(movement, 1f);

        Transform cam_tf = cine_cam.transform;
        Vector3 cam_fwd = cam_tf.forward;
        Vector3 cam_right = cam_tf.right;

        cam_fwd.y = 0f;
        cam_right.y = 0f;
        cam_fwd.Normalize();
        cam_right.Normalize();

        Vector3 world_move = cam_right * movement.x + cam_fwd * movement.z;
        world_move = Vector3.ClampMagnitude(world_move, 1f);

        if (world_move.sqrMagnitude > 0.0001f)
        {
            float rot_speed = 10f;
            Vector3 desired_direction = world_move.normalized;
            Quaternion target_rot = Quaternion.LookRotation(desired_direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                target_rot,
                Time.deltaTime * rot_speed
            );
        }

        if (jump.triggered && grounded)
        {
            velocity.y = Mathf.Sqrt(jump_height * -2f * gravity_value);
        }

        velocity.y += gravity_value * Time.deltaTime;

        Vector3 final_move = world_move * player_speed + velocity.y * Vector3.up;
        character_controller.Move(final_move * Time.deltaTime);
    }
}
