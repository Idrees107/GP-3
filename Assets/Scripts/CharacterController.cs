using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterControllerTopDown : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    private Vector2 moveInput;

    [Header("Look")]
    [SerializeField] float rotationSpeed = 600f;
    private Vector2 lookInput;

    [Header("Mouse")]
    [SerializeField] Camera mainCamera;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerInput = GetComponent<PlayerInput>();

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

  
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.linearVelocity = move * moveSpeed;

        
        Vector3 lookDir = new Vector3(lookInput.x, 0f, lookInput.y);

        if (lookDir.sqrMagnitude > 0.001f)
        {
                        Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        }
        else if (playerInput.playerIndex == 0 && Mouse.current != null)
        {
            
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane plane = new Plane(Vector3.up, transform.position);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPoint = ray.GetPoint(distance);
                Vector3 mouseDir = worldPoint - transform.position;
                mouseDir.y = 0f;
                if (mouseDir.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(mouseDir, Vector3.up);
                    rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }
}
