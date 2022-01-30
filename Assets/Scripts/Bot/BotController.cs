using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BotState
{
    STARTING,
    ENGAGE,
    IN_ACTIVE,
    DEFEATED
}

public class BotController : MonoBehaviour
{
    //Note, for our game the target will be the other bot, probably always.
    //Body parts will grab this as needed to take actions on targets (aim, do damage, fire projectiles, etc.)
    //If we ever have more than two bots or more than two targets we may need to change this to an interface.
    public BotController target;

    //Keeping things flexible with a list of body parts for now.
    public BodyPart[] activeParts;

    public BodyPart[] allParts;

    private BotState state = BotState.STARTING;

    public Collider[] childrenColliders;

    public BotState State // property
    {
        get { return state; } // get method
        //set { name = value; }  // set method
    }

    private int _totalHitpoints = 0;
    private int _totalDamage = 0;

    //TODO:  This will be calculated from the weapons.
    public float desiredDistance = 15f;
    public float acceptableDistnaceRange = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        childrenColliders = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        if (_totalDamage >= _totalHitpoints)
        {
            Lose();
        }
    }

    public float GetDamage()
    {
        return _totalDamage;
    }

    //Even though we calculate damage per piece,
    //this accumulates global damage for if we want to show a health bar etc.
    public void TakeDamage(int damage)
    {
        _totalDamage += damage;
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

    public void Lose()
    {
        if (state == BotState.ENGAGE)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            foreach (BodyPart b in activeParts)
            {
                b.Deactivate();
                b.gameObject.AddComponent<Rigidbody>();
            }

            state = BotState.DEFEATED;
        }
    }

    public void Win()
    {
        foreach (BodyPart b in activeParts)
        {
            b.Deactivate();
        }
    }
}