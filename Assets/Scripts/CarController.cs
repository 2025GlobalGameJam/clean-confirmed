using System;
using UnityEngine;
using UnityEngine.InputSystem;

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


    InputActionMap map;
    InputAction move;

    void Start()
    {
        theRB.transform.parent = null;

        dragOnGround = theRB.linearDamping;
        map = GetComponent<PlayerInput>().currentActionMap;
        move = map.FindAction("Move");
    }

    void Update()
    {
        speedInput = 0f;
        if (move.ReadValue<Vector2>().y > 0)
        {
            speedInput = move.ReadValue<Vector2>().y * forwardAccel;
        }
        else if (move.ReadValue<Vector2>().y < 0)
        {
            speedInput = move.ReadValue<Vector2>().y * reverseAccel;

        }

        turnInput = move.ReadValue<Vector2>().x;
        
     if(move.ReadValue<Vector2>().y != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,turnInput * turnStrength * Time.deltaTime * MathF.Sign(speedInput) * (theRB.linearVelocity.magnitude / maxSpeed), 0f));
        }

        //control particle emissions
        emissionRate = Mathf.MoveTowards(emissionRate, 25f, emissionFadeSpeed * Time.deltaTime);

        if (grounded)
        {
            emissionRate = maxEmissions;
        }

        for(int i = 0; i < dustTrail.Length; i++)
        {
            var emissionModule = dustTrail[i].emission;

            emissionModule.rateOverTime = emissionRate;
        }

        if(theRB.linearVelocity.magnitude > maxSpeed)
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

        //accelerates the car
        if (grounded)
        {
            theRB.linearDamping = dragOnGround;

            theRB.AddForce(transform.forward * speedInput * 1000f);
        }
        
        theRB.AddForce(transform.forward * speedInput * 1000f);
        transform.position = theRB.position;


    }
    
}
