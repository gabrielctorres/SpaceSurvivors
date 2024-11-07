using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterMoviment : MonoBehaviour
{

    [Header("Movement Settings")]
    public float thrustForce = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {

        rb.AddForce(moveInput.normalized * thrustForce, ForceMode2D.Force);
    }

}
