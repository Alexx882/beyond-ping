using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject commander;
    public bool alive = true;
    public float maxDelay = 1.5f;
    
    private Queue<(double,Vector2)> movementQueue;
    Rigidbody2D rb;
    InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        movementQueue = new Queue<(double,Vector2)>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Respawn();
        }

        MoveOnInput();
        
        PointInDirectionOfVelocity();
    }

    private void MoveOnInput()
    {
        if (alive)
        {
            if (moveAction.ReadValue<Vector2>() != Vector2.zero)
            { 
                movementQueue.Enqueue((Time.time * 1000, moveAction.ReadValue<Vector2>()));
            }

            if (movementQueue.Count > 0)
            {
                float delay = GetCurrentDelayInMilliseconds();
                
                if (movementQueue.Peek().Item1 + delay < Time.time * 1000)
                {
                    rb.AddForce(movementQueue.Dequeue().Item2 * moveSpeed);
                }
            }
        }
    }

    private void PointInDirectionOfVelocity()
    {
        if (alive)
        {
            // todo
            // Calculate the angle from the velocity direction
            float targetAngle = Mathf.Atan2(-rb.linearVelocity.y, -rb.linearVelocity.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle+90); // Keep rotation on Z-axis
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
        if (true)
            return;

        Vector2 moveValue = value.Get<Vector2>();
        Debug.Log(moveValue);
        rb.AddForce(moveValue * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("DEAD");
            alive = false;
        }
    }


    float GetDistanceToCommander()
    {
        return (commander.transform.position - this.transform.position).magnitude;
        // Debug.Log("Distance: " + distance);
    }

    float GetCurrentDelayInMilliseconds()
    {   
        //Debug.Log(GetDistanceToCommander());
        return maxDelay / 100 * GetDistanceToCommander() * 1000;
    }
}