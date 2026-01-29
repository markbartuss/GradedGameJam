using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInventory;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions controls; 
    private CharacterController controller;

    public Transform cameraTransform;
    private PlayerInventory inventory;
    private Vector2 moveInput; private Vector3 velocity;

    private PlayerInventory.AbilityType currentAbility = PlayerInventory.AbilityType.None;
    public PlayerInventory.AbilityType CurrentAbility => currentAbility;

    [Header("Movement")] public float moveSpeed = 6f; 
    public float gravity = -9.81f; 
    public float jumpHeight = 1.5f;
    [Header("Slimes")]
    public GameObject slime;
    public GameObject frog;
    public GameObject rabbit;

    [Header("Jump")] 
    public int maxJumps = 2; 
    private int jumpCount = 0;

    [Header("Dash")] 
    public float dashSpeed = 20f; 
    public float dashDuration = 0.2f; 
    private bool isDashing = false; 
    private float dashTimer = 0f;
    public float dashCooldown = 1f; 
    private float dashCooldownTimer = 0f;


    private void Awake() 
    { 
        controls = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<PlayerInventory>();
        //this uses the new input system which includes all the keybind for basic movement
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
        if (Keyboard.current.digit1Key.wasPressedThisFrame)//equips the ability thats in slot 1 -> slot 2 for the one below
        {
            currentAbility = inventory.slot1;

            if(currentAbility == PlayerInventory.AbilityType.Jump)
            {
                frog.SetActive(true);
                slime.SetActive(false);
                rabbit.SetActive(false);
            }
            else if (currentAbility == PlayerInventory.AbilityType.Dash)
            {
                frog.SetActive(false);
                slime.SetActive(false);
                rabbit.SetActive(true);
            }
            else
            {
                frog.SetActive(false);
                slime.SetActive(true);
                rabbit.SetActive(false);
            }    
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentAbility = inventory.slot2;

            if (currentAbility == PlayerInventory.AbilityType.Jump)
            {
                frog.SetActive(true);
                slime.SetActive(false);
                rabbit.SetActive(false);
            }
            else if (currentAbility == PlayerInventory.AbilityType.Dash)
            {
                frog.SetActive(false);
                slime.SetActive(false);
                rabbit.SetActive(true);
            }
            else
            {
                frog.SetActive(false);
                slime.SetActive(true);
                rabbit.SetActive(false);
            }
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

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

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;// goes forward where the camera is looking
        move.y = 0f;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;
        }

        

        if (move != Vector3.zero)//rotates the player to face towards the direction player is heading in 
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
        if (currentAbility != PlayerInventory.AbilityType.Jump) return;//changes the ability type/ checks if slot 1 or 2/ if jump is equiped
        if (!inventory.hasJumpItem) return;

        if (jumpCount < maxJumps)// jumps is limited to 2
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);//makes the player go in the air/jump
            jumpCount++;

        }   
    }
    private void Dash()
    {
        if (currentAbility != PlayerInventory.AbilityType.Dash) return;//changes the ability type/ checks if slot 1 or 2/ if dash is equiped
        if (!inventory.hasDashItem) return; 
        if (isDashing) return;
        if (dashCooldownTimer > 0f) return;//dash cooldown 

        velocity.y = 0f;
        isDashing = true; 
        dashTimer = dashDuration;

        dashCooldownTimer = dashCooldown;//resets dash
    }
}
