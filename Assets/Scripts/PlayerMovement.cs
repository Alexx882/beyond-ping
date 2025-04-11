using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject commander;
    public bool alive = true;

    Rigidbody2D rb;
    InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Respawn();
        }

        // todo controller input
        // Vector2 moveValue = moveAction.ReadValue<Vector2>();
        // Debug.Log(moveValue);
        // rb.AddForce(moveValue * moveSpeed);
        // rb.linearVelocity = moveValue * moveSpeed;

        GetDistanceToCommander();

        PointInDirectionOfVelocity();
    }

    private void PointInDirectionOfVelocity()
    {
        if (alive)
        {
            // todo
            // Calculate the angle from the velocity direction
            // float targetAngle = Mathf.Atan2(-rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;

            // transform.rotation = Quaternion.Euler(0f, 0f, targetAngle+90); // Keep rotation on Z-axis
        }
    }

    private void Respawn()
    {
        this.transform.position = new Vector3(0, 3, 0);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;
        this.transform.rotation = Quaternion.identity;
        alive = true;
    }

    /**
     * Triggered with the new Input System
     */
    void OnMove(InputValue value)
    {
        if (!alive)
            return;

        Vector2 moveValue = value.Get<Vector2>();
        // Debug.Log(moveValue);
        rb.linearVelocity = moveValue * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("DEAD");
            alive = false;
        }
    }


    void GetDistanceToCommander()
    {
        var distance = (commander.transform.position - this.transform.position).magnitude;
        Debug.Log("Distance: " + distance);
    }
}