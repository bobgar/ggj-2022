using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : BodyPart
{
    //TODO will we have turning?
    public float turnspeed = 1.0f;


    public float accelerationForward = .0005f;
    public float accelerationReverse = .00025f;
    protected Vector3 velocity;
    public float maxVelocity = .15f;
    protected float dampening = .985f;

    protected float jumpchance = .0001f;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (botController.State == BotState.FIGHTING)
        {
            Move();
        }
    }

    protected void Move()
    {
        Vector3 distance = botController.target.transform.position - botController.transform.position;

        Vector3 acceleration = Vector3.Normalize(distance);
        acceleration.y = 0;
        //TODO do we want any turning?
        //Vector3 movement = botController.transform.forward;

        if (distance.magnitude > botController.GetDesiredMaxDistance())
        {
            acceleration *= accelerationForward;
            velocity += acceleration;
            //botController.transform.position += movement;
        }
        else if (distance.magnitude < botController.GetDesiredMinDistance())
        {
            acceleration *= -accelerationReverse;
            velocity += acceleration;
            //botController.transform.position += movement;
        }

        if (Random.value < jumpchance)
        {
            velocity += new Vector3(0f, .05f, 0f);
        }


        botController.transform.position += velocity;
        velocity = velocity * dampening;
    }
}