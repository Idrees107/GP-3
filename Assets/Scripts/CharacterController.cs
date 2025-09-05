using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CharacterControllerRB : MonoBehaviour
{
    private PlayerInputActions input;
    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    private Vector2 moveInput;

    
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    private bool IsGrounded => Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

    [Header("Camera Look")]
    [SerializeField] float lookSensitivity = 3f;
    [SerializeField] float minPitch = -30f;
    [SerializeField] float maxPitch = 60f;
    [SerializeField] float cameraDistance = 5f;
    [SerializeField] Transform cameraTransform;

    private float yaw;
    private float pitch = 20f;
    private Vector2 lookInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        input = new PlayerInputActions();
        input.Player.Enable();

        
        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Jump.performed += OnJump;

        input.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnDisable()
    {
        input.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled -= ctx => moveInput = Vector2.zero;

        input.Player.Jump.performed -= OnJump;

        input.Player.Look.performed -= ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled -= ctx => lookInput = Vector2.zero;

        input.Player.Disable();
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * moveInput.y + camRight * moveInput.x;
        moveDir.Normalize();

     
        Vector3 targetVelocity = moveDir * moveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        
    }

    void LateUpdate()
    {
        if (!cameraTransform) return;

        
        yaw += lookInput.x * lookSensitivity * Time.deltaTime;
        pitch -= lookInput.y * lookSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

       
        Vector3 offset = rotation * new Vector3(0, 0, -cameraDistance);
        cameraTransform.position = transform.position + offset;
        cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
