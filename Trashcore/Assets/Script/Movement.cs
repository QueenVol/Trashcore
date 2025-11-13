using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 0.1f;

    private Rigidbody rb;
    private Transform cam;

    private PlayerInputActions input;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool jumpPressed;

    private float xRotation = 0f;
    private bool isGrounded;

    void Awake()
    {
        input = new PlayerInputActions();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        input.Player.Jump.performed += ctx => jumpPressed = true;
    }

    void OnEnable() => input.Player.Enable();
    void OnDisable() => input.Player.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        Vector3 dir = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 vel = dir.normalized * moveSpeed;

        rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);
    }

    void HandleLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    void HandleJump()
    {
        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        jumpPressed = false;
    }

    void OnCollisionStay(Collision collision) => isGrounded = true;
    void OnCollisionExit(Collision collision) => isGrounded = false;
}
