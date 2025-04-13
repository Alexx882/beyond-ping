using System;
using System.Diagnostics;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = .5f;
    public GameObject commander;
    public float maxDelay = 1.5f;
    private Queue<(double, Vector2)> movementQueue;
    public bool hasTakenOff = false;
    public bool isAlive = true;
    public GameObject thruster;
    Rigidbody2D rb;
    public TrailRenderer trailRenderer;
    public float maxVelocity = 100.0f;

    private GameStateScript gameStateScript;
    private ObjectiveIndicator objectiveIndicator;

    public float maxThrusterAnimationDelay = 0.1f;
    private float _lastInputTime;
    private Vector2 _lastInput;
    private ParticleSystem _thrusterParticleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStateScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateScript>();
        
        rb = GetComponent<Rigidbody2D>();
        movementQueue = new Queue<(double, Vector2)>();
        trailRenderer = this.GetComponent<TrailRenderer>();
        _thrusterParticleSystem = thruster.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Respawn();
        }

        MoveOnInput();

        // PointInDirectionOfVelocity();

        thruster.transform.position = transform.position;
        PointThrusterInInputDirection();
    }

    private void MoveOnInput()
    {
        if (isAlive)
        {
            // Vector2 moveInputVector = moveAction.ReadValue<Vector2>();
            Vector2 moveInputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            // TODO FUCK MAC
            moveInputVector.y = -moveInputVector.y;
#endif

            if (moveInputVector != Vector2.zero)
            {
                movementQueue.Enqueue((Time.time * 1000, moveInputVector));
            }

            while (movementQueue.Count > 0 &&
                   movementQueue.Peek().Item1 + GetCurrentDelayInMilliseconds() < Time.time * 1000)
            {
                Vector2 moveValue = movementQueue.Dequeue().Item2;
                if (!hasTakenOff && moveValue.magnitude > 0.1f)
                {
                    hasTakenOff = true;
                    trailRenderer.emitting = true;
                }

                rb.AddForce(moveValue * moveSpeed);
                rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);

                _lastInput = moveValue;
                _lastInputTime = Time.time;
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

    private void PointThrusterInInputDirection()
    {
        if (Time.time > _lastInputTime + maxThrusterAnimationDelay)
        {
            var emission = _thrusterParticleSystem.emission;
            emission.enabled = false;
            _lastInput = Vector2.zero;
        }
        else if (_lastInput != Vector2.zero)
        {
            var emission = _thrusterParticleSystem.emission;
            emission.enabled = true;
            var thrusterAngle = Mathf.Atan2(-_lastInput.y, -_lastInput.x) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.Euler(0f, 0f, thrusterAngle + 90);
            thruster.transform.rotation =
                Quaternion.RotateTowards(thruster.transform.rotation, targetRotation, Time.deltaTime * 1000f);
        }
    }

    private void Respawn()
    {
        gameStateScript.Reset();
        
        
        // reset internal state
        trailRenderer.emitting = false;
        hasTakenOff = false;
        movementQueue.Clear();

        // reset world position
        this.transform.position = new Vector3(0, 3, 0);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;
        this.transform.rotation = Quaternion.identity;

        isAlive = true;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGO = other.gameObject;
        if (otherGO.CompareTag("Collectible"))
        {
            otherGO.GetComponent<Collectible>().Collect();
        }
    }

    public float GetDistanceToCommander()
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