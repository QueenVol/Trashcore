using UnityEngine;

public class Flight : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInputActions input;

    private bool isFlying = false;
    private float flyForce;
    private float flyEndTime;

    private bool jumpHeld;

    void Awake()
    {
        input = new PlayerInputActions();
        input.Player.Jump.performed += ctx => jumpHeld = true;
        input.Player.Jump.canceled += ctx => jumpHeld = false;
    }

    void OnEnable() => input.Player.Enable();
    void OnDisable() => input.Player.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateFlight(float duration, float force)
    {
        isFlying = true;
        flyForce = force;
        flyEndTime = Time.time + duration;
    }

    void FixedUpdate()
    {
        if (!isFlying) return;

        if (Time.time >= flyEndTime)
        {
            isFlying = false;
            return;
        }

        if (jumpHeld)
        {
            rb.AddForce(Vector3.up * flyForce, ForceMode.Acceleration);
        }
    }
}
