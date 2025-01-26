using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CarController : MonoBehaviour
{
   public Rigidbody theRB;
   public float maxSpeed;
   public float forwardAccel = 0f, reverseAccel = 0f;
   private float speedInput;
   public float turnStrength = 0f;
   private float turnInput;


    void Start()
    {
        theRB.transform.parent = null;
    }
        void Update()
    {
        speedInput = 0f;
        if (Input.GetAxis ("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel;

        }

        turnInput =Input.GetAxis("Horizontal");

        // if(Input.GetAxis("Vertical") != 0)
        // {
        //     transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,turnInput * turnStrength * Time.deltaTime * MathF.Sign(speedInput) * (theRB.linearVelocity.magnitude / maxSpeed)  ,0f));
        // }


        //transform.position = theRB.position;
    }

    private void FixedUpdate()
    {
        theRB.AddForce(transform.forward * speedInput * 500f);
        transform.position = theRB.position;

     if(Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,turnInput * turnStrength * Time.deltaTime * MathF.Sign(speedInput) * (theRB.linearVelocity.magnitude / maxSpeed)  ,0f));
        }
    }
    //Bubble Code?
    // public ParticleSystem popEffect;

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Bubble"))
    //     {
    //         PopBubble();
    //     }
    // }

    // void PopBubble()
    // {
    //     Instantiate(popEffect, transform.position, Quaternion.identity);
    //     Destroy(gameObject);
    // }

}
