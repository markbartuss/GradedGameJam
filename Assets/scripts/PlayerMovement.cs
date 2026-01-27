using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions controls; 
    private CharacterController controller;

    public Transform cameraTransform;
    private PlayerInventory inventory;
    private Vector2 moveInput; private Vector3 velocity;

    [Header("Movement")] public float moveSpeed = 6f; 
    public float gravity = -9.81f; 
    public float jumpHeight = 1.5f;

    [Header("Jump")] 
    public int maxJumps = 2; 
    private int jumpCount = 0;

    [Header("Dash")] 
    public float dashSpeed = 20f; 
    public float dashDuration = 0.2f; 
    private bool isDashing = false; 
    private float dashTimer = 0f;

    private void Awake() 
    { 
        controls = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<PlayerInventory>();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); 
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Dash.performed += ctx => Dash();
        controls.Player.Jump.performed += ctx => Jump();
    }
    private void OnEnable() 
    { 
        controls.Enable(); 
    }
    private void OnDisable() 
    { 
        controls.Disable();
    }

    private void Update()
    {

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            Vector3 dashDir = transform.forward * dashSpeed;
            controller.Move(dashDir * Time.deltaTime);

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }

            return;
        }

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;
        move.y = 0f;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;
        }

        

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(move * moveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
    private void Jump() 
    {
        if (!inventory.hasJumpItem) return;

        if (jumpCount < maxJumps)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount++;

        }   
    }
    private void Dash()
    {
        if (!inventory.hasDashItem) return; 
        if (isDashing) return; 

        isDashing = true; 
        dashTimer = dashDuration; 
    }
}
