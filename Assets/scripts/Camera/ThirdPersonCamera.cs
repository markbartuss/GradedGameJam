using UnityEngine;
using UnityEngine.InputSystem;
public class ThirdPersonCamera : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Vector2 lookInput;
    public Transform target;
    [Header("Camera")]

    public float distance = 3f;
    public float sensitivity = 0.5f;
    public float minY = -30f;
    public float maxY = 70f;

    private float rotX;
    private float rotY;
    private void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void LateUpdate()
    {
        if (target == null) return;
        {
            rotX += lookInput.x * sensitivity;
            rotY -= lookInput.y * sensitivity;
            rotY = Mathf.Clamp(rotY, minY, maxY);

            transform.rotation = Quaternion.Euler(rotY, rotX, 0);

            transform.position = target.position;
        }
    }
}
