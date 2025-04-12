using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = .5f;
    public GameObject commander;
    public float maxDelay = 1.5f;
    private Queue<(double,Vector2)> movementQueue;
    public bool hasTakenOff = false;
    public bool isAlive = true;
    Rigidbody2D rb;
    InputAction moveAction;
    TrailRenderer trailRenderer;
    public float maxVelocity = 100.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        movementQueue = new Queue<(double,Vector2)>();
        trailRenderer = this.GetComponent<TrailRenderer>();
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
        if (isAlive)
        {
            if (moveAction.ReadValue<Vector2>() != Vector2.zero)
            { 
                movementQueue.Enqueue((Time.time * 1000, moveAction.ReadValue<Vector2>()));
            }
            
            while (movementQueue.Count > 0 && movementQueue.Peek().Item1 + GetCurrentDelayInMilliseconds() < Time.time * 1000)
            {
                Vector2 moveValue = movementQueue.Dequeue().Item2;
                if (!hasTakenOff && moveValue.magnitude > 0.1f)
                {
                    hasTakenOff = true;
                    trailRenderer.emitting = true;
                }
                rb.AddForce(moveValue * moveSpeed);
                rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
                
            }
            
            
        }
    }

    private void PointInDirectionOfVelocity() 
    {
        if (isAlive)
        {
            // Calculate the angle from the velocity direction
            float targetAngle = Mathf.Atan2(-rb.linearVelocity.y, -rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle + 90); // Keep rotation on Z-axis
        }
    }

    private void Respawn()
    {
        trailRenderer.emitting = false;
        hasTakenOff = false;
        this.transform.position = new Vector3(0, 3, 0);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;
        this.transform.rotation = Quaternion.identity;

        isAlive = true;
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
            isAlive = false;
            trailRenderer.emitting = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var otherGO = other.gameObject;
        if (otherGO.CompareTag("GravityRadius"))
        {
            // inside a planet's gravity
            Vector2 direction = otherGO.transform.position - transform.position;
            float distanceSqr = direction.sqrMagnitude;
            
            if (distanceSqr == 0f) return;
            
            var gravityConstant = 10f;
            
            float forceMagnitude = gravityConstant * (otherGO.GetComponentInParent<PlanetGravity>().planetMass * 1) / distanceSqr;
            Vector2 force = direction.normalized * forceMagnitude;

            rb.AddForce(force);
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