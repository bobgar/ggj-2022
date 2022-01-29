using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BodyPartState
{
    NORMAL,
    MALFUNCTIONING, //TODO cool idea?  separate function when in bad state somehow?
    DESTROYED,
}

public class BodyPart : MonoBehaviour
{
    public BotController botController;
    //Set the default maxHitpoints to 100, overridable by the inspector
    public int maxHitpoints = 100;
    protected int hitpoints;
    public BodyPartState state;

    // Start is called before the first frame update
    public void Start()
    {        
        hitpoints = maxHitpoints;
        botController = GetComponentInParent<BotController>();
        botController.AddHitpoints(maxHitpoints);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //If a body part is destroyed, do whatever is involved.  Play animation?  disable functionality?  etc.
    protected void Destroy()
    {
        state = BodyPartState.DESTROYED;
        //TODO should be virtual?  will be implemented specific to pieces I think.
    }

    public void TakeDamage(int damage)
    {
        //If we're destroyed, ignore.
        if (state == BodyPartState.DESTROYED)
            return;

        //Cap damage at as many hitpoints as we have left.
        if (damage > hitpoints)
        {
            damage = hitpoints;
        }

        //Apply the damage locally
        hitpoints -= damage;
        Debug.Log(botController.name + "'s " + gameObject.name + " received " + damage + " damage at " +
                  DateTime.Now.Ticks + " current HP: " + hitpoints);
        //Apply destruction
        if (hitpoints <= 0)
        {
            Destroy();
        }

        botController.TakeDamage(damage);
    }

    public void HealDamage(int healing)
    {
        //If we're destroyed, ignore.
        if (state == BodyPartState.DESTROYED)
            return;

        //Cap healing to what it would take to reach max hitpoints
        if (hitpoints + healing > maxHitpoints)
        {
            healing = maxHitpoints - hitpoints;
        }

        hitpoints += healing;
        //Update the total hitpoints on controller
        botController.AddHitpoints(healing);
    }
}