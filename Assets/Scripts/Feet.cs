using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : BodyPart
{
    //public Rigidbody rigidbody;
    public float speed = 1.0f;
    public float turnspeed = 1.0f;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
    }

    Vector3 m_NewForce;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected void Move()
    {
        Vector3 movement = Vector3.Normalize(botController.transform.position - botController.target.transform.position);

    }
}
