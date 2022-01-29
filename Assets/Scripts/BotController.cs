using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    //Note, for our game the target will be the other bot, probably always.
    //Body parts will grab this as needed to take actions on targets (aim, do damage, fire projectiles, etc.)
    //If we ever have more than two bots or more than two targets we may need to change this to an interface.
    public BotController target;
    //I'm not sure a public link to a rigid body is needed anymore, but here it is!
    public Rigidbody rigidbody;
    //Keeping things flexible with a list of body parts for now.
    public BodyPart[] bodyParts;

    private int _totalHitpoints = 0;
    private int _totalDamage = 0;

    //TODO:  This will be calculated from the weapons.
    private float desiredDistance = 4f;
    private float acceptableDistnaceRange = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Even though we calculate damage per piece,
    //this accumulates global damage for if we want to show a health bar etc.
    public void TakeDamage(int damage)
    {
        _totalDamage += damage;
        if(_totalDamage <= 0)
        {
            //TODO bot is destoryed, end the match or something
        }
    }

    public void AddHitpoints(int hitpoints)
    {
        _totalHitpoints += hitpoints;
    }

    public float GetDesiredMinDistance()
    {
        return desiredDistance - acceptableDistnaceRange;
    }

    public float GetDesiredMaxDistance()
    {
        return desiredDistance + acceptableDistnaceRange;
    }
}
