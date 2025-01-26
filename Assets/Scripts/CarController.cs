using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody theRB;
    public float maxSpeed;
    public float forwardAccel = 0f, reverseAccel = 0f;
    private float speedInput;
    public float turnStrength = 0f;
    private float turnInput;
    public ParticleSystem[] dustTrail;
    public float maxEmissions = 25f, emissionFadeSpeed = 20f;
    private float emissionRate;
    public bool grounded;
    public Transform groundRayPoint;
    public LayerMask WhatIsGround;
    public float groundRayLength = 0.75f;
    private float dragOnGround;

    // Added audio variables
    public AudioClip idleEngineSound;
    public AudioClip dashEngineSound;   
    public AudioClip driftSound;
    public AudioClip crashSound;
    public AudioClip bubblePopSound;

    private AudioSource engineSource;
    private AudioSource driftSource;
    private bool isMoving = false; // Moving check variables

    void Start()
    {
        theRB.transform.parent = null;
        dragOnGround = theRB.linearDamping;

        // Add audio source
        engineSource = gameObject.AddComponent<AudioSource>();
        driftSource = gameObject.AddComponent<AudioSource>();

        // Set engine sound
        engineSource.clip = idleEngineSound;
        engineSource.loop = true;
        engineSource.playOnAwake = false;
        engineSource.volume = 0.3f; // Setting the default volume

        // Set drift sound
        driftSource.clip = driftSound;
        driftSource.loop = true;
        driftSource.playOnAwake = false;
        driftSource.volume = 0f; // Mute the sound initially

        engineSource.Play();
        driftSource.Play();
    }

    void Update()
    {
        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel;
        }

        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * MathF.Sign(speedInput) * (theRB.linearVelocity.magnitude / maxSpeed), 0f));
        }

        // Update engine sound
        UpdateEngineSound();

        // Update drift sound
        UpdateDriftSound();

        // Control particle
        emissionRate = Mathf.MoveTowards(emissionRate, 25f, emissionFadeSpeed * Time.deltaTime);
        if (grounded)
        {
            emissionRate = maxEmissions;
        }

        for (int i = 0; i < dustTrail.Length; i++)
        {
            var emissionModule = dustTrail[i].emission;
            emissionModule.rateOverTime = emissionRate;
        }

        if (theRB.linearVelocity.magnitude > maxSpeed)
        {
            theRB.linearVelocity = theRB.linearVelocity.normalized * maxSpeed;
        }
    }

    private void FixedUpdate()
    {
        grounded = false;

        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, WhatIsGround))
        {
            grounded = true;
        }

        if (grounded)
        {
            theRB.linearDamping = dragOnGround;
            theRB.AddForce(transform.forward * speedInput * 1000f);
        }

        theRB.AddForce(transform.forward * speedInput * 1000f);
        transform.position = theRB.position;
    }

    // Engine sound(Idle & Dash)
    private void UpdateEngineSound()
    {
        float speedMagnitude = theRB.linearVelocity.magnitude;
        bool isCurrentlyMoving = speedMagnitude > 0.5f; // 속도가 0.5 이상이면 움직이는 것으로 판단

        if (isCurrentlyMoving != isMoving) // 상태 변화가 있을 경우만 처리
        {
            isMoving = isCurrentlyMoving;
            if (isMoving)
            {
                engineSource.clip = dashEngineSound;  // 주행 소리로 변경
            }
            else
            {
                engineSource.clip = idleEngineSound; // 다시 Idle 사운드로 변경
            }
            engineSource.Play();
        }

        // 속도에 따른 엔진 사운드 피치 조절
        engineSource.pitch = Mathf.Lerp(0.8f, 2.0f, speedMagnitude / maxSpeed);
    }

    // Drift sound
    private void UpdateDriftSound()
    {
        bool isDrifting = Mathf.Abs(turnInput) > 0.3f && Mathf.Abs(speedInput) > 0.2f; // Above a certain speed + changing direction is considered drifting
        driftSource.volume = isDrifting ? 0.8f : Mathf.Lerp(driftSource.volume, 0f, Time.deltaTime * 5f); // Tapered effect
    }

    // Crash sound
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            // Play a popping sound when you collide with a Bubble
            AudioSource.PlayClipAtPoint(bubblePopSound, transform.position, 1.0f);
        }
        else
        {
            // Play normal sound effects at the moment of collision
            AudioSource.PlayClipAtPoint(crashSound, transform.position, 1.0f);
        }
    }
}