using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] Transform target;   
    [Header("Settings")]
    [SerializeField] float sensitivity = 3f;
     float distance = 5f;
     float minPitch = -30f;
     float maxPitch = 60f;

    private float yaw;
    private float pitch;

    private PlayerInputActions input;
    private Vector2 lookInput;

    void Awake()
    {
        input = new PlayerInputActions();
        input.Player.Enable();

        input.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnDisable()
    {
        input.Player.Look.performed -= ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled -= ctx => lookInput = Vector2.zero;
        input.Player.Disable();
    }

    void LateUpdate()
    {
        if (!target) return;

        
        yaw += lookInput.x * sensitivity * Time.deltaTime;
        pitch -= lookInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Set camera position behind target
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = target.position + offset;

        // Always look at target
        //  transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}