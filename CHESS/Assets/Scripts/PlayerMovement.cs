using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour {
    CharacterController character;
    Vector3 velocityVector;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpAmount = 5f;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    bool isMoving = false;
    bool isRotating = false;
    bool isJumping = false;


    // Start is called before the first frame update
    void Start() {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        groundedPlayer = character.isGrounded;
        if (groundedPlayer && velocityVector.y < 0) {
            velocityVector.y = 0f;
        }
        if (isMoving) {
            character.Move(velocityVector * speed * Time.fixedDeltaTime);
            isMoving = false;
        }

        if (isJumping && groundedPlayer) {
            velocityVector.y += Mathf.Sqrt(jumpAmount * -3.0f * gravityValue);
            isJumping = false;
        }
        velocityVector.y += gravityValue * Time.fixedDeltaTime;
        character.Move(velocityVector * Time.deltaTime);


    }

    public void OnMovementChanged( InputAction.CallbackContext context ) {
        isMoving = true;
        Vector2 direction = context.ReadValue<Vector2>();
        velocityVector = new Vector3(direction.x, 0, direction.y);
        Debug.Log("Move");
    }

    public void OnCameraRotationChanged(InputAction.CallbackContext context ) {
        isRotating = true;
    }

    public void OnJumpChanged( InputAction.CallbackContext context ) {
        isJumping = true;
    }
}