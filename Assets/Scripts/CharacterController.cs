using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private PlayerInputActions characterInputMap;

    Rigidbody characterRBG;

    [SerializeField] float speedMultiplier;
    [SerializeField] float jumpForce;

    Vector3 movementVector = new Vector3(0, 0, 0);


    void Awake()
    {
        characterInputMap = new PlayerInputActions();

        characterInputMap.Enable();

       // characterInputMap.Player.Jump.performed += OnJump;
       // characterInputMap.Player.Jump.canceled -= OnJump;

        //characterInputMap.Player.Move.performed += x => OnPlayerMove(x.ReadValue<Vector2>());
        characterInputMap.Player.Move.canceled += x => OnPlayerStopMove(x.ReadValue<Vector2>());

        if (characterRBG == null)
        {
            characterRBG = GetComponent<Rigidbody>();
        }
    }

    void OnDisable()
    {
        //characterInputMap.Player.Move.performed -= x => OnPlayerMove(x.ReadValue<Vector2>());
        characterInputMap.Player.Move.canceled -= x => OnPlayerStopMove(x.ReadValue<Vector2>());

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        characterRBG.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        characterRBG.AddForce(movementVector * speedMultiplier, ForceMode.Force);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementVector = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    private void OnPlayerStopMove(Vector2 incomingVector2)
    {
        movementVector = new Vector3(0, 0, 0);
    }
}
