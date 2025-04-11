using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    
    Rigidbody2D rb;
    InputAction moveAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move Simple");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        // Debug.Log(moveValue);
        // rb.AddForce(moveValue * moveSpeed);
        // rb.linearVelocity = moveValue * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        Vector2 moveValue = value.Get<Vector2>();
        Debug.Log(moveValue);
        rb.linearVelocity = moveValue * moveSpeed;

    }
    
    
}
