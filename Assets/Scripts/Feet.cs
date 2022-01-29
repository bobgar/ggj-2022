using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : BodyPart
{
    //TODO will we have turning?
    public float turnspeed = 1.0f;


    private float accelerationForward = .0001f;
    private float accelerationReverse = .00005f;
    protected Vector3 velocity;
    private float maxVelocity = .03f;
    protected float dampening = .985f;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected void Move()
    {        
        Vector3 distance = botController.target.transform.position - botController.transform.position;

        Vector3 acceleration = Vector3.Normalize(distance);
        //TODO do we want any turning?
        //Vector3 movement = botController.transform.forward;

        if(distance.magnitude > botController.GetDesiredMaxDistance() )
        {
            acceleration *= accelerationForward;
            velocity += acceleration;
            //botController.transform.position += movement;
        }
        else if(distance.magnitude < botController.GetDesiredMinDistance() )
        {
            acceleration *= -accelerationReverse;
            velocity += acceleration;
            //botController.transform.position += movement;
        }
        
        botController.transform.position += velocity;
        velocity = velocity * dampening;
    }
}
