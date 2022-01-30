using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public enum BotState
{
    STARTING,
    FIGHTING,
    VICTORIOUS,
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
    
    public Dictionary<Part, BodyPart> parts = new Dictionary<Part, BodyPart>();

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

        foreach(BodyPart b in allParts)
        {            
            switch (b.name){
                case "Chest Piece":
                    parts.Add(Part.CHEST, b);
                    break;
                case "Head_Basic":
                    parts.Add(Part.BASIC_HEAD, b);
                    break;
                case "Wheels_2 pair":
                    parts.Add(Part.WHEELS, b);
                    break;
                case "Tank Threads":
                    parts.Add(Part.TANK_TREADS, b);
                    break;
                case "Sythe Arms L":
                    parts.Add(Part.SYTHE_ARM_LEFT, b);
                    break;
                case "Sythe Arms R":
                    parts.Add(Part.SYTHE_ARM_RIGHT, b);
                    break;
                case "Windmill Arm L":
                    parts.Add(Part.WINDMILL_ARM_LEFT, b);
                    break;
                case "Windmill Arm R":
                    parts.Add(Part.WINDMILL_ARM_RIGHT, b);
                    break;
                case "Hammer Hand L":
                    parts.Add(Part.HAMMER_ARM_LEFT, b);
                    break;
                case "Hammer Hand R":
                    parts.Add(Part.HAMMER_ARM_RIGHT, b);
                    break;
                case "Dragon Claw Feet":
                    parts.Add(Part.DRAGON_CLAW_FEET, b);
                    break;
                case "Cannon Head":
                    parts.Add(Part.TANK_HEAD, b);
                    break;
            }
            b.gameObject.SetActive(false);
        }
        //Set the CHEST to active.
        parts[Part.CHEST].gameObject.SetActive(true);
    }

    public void AddPiece(Part part)
    {
        if (parts.ContainsKey(part))
        {
            parts[part].gameObject.SetActive(true);
        }
    }

    public void RemovePiece(Part part)
    {
        if (parts.ContainsKey(part)){
            parts[part].gameObject.SetActive(false);
        }
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
        if (state == BotState.FIGHTING)
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
        if (state == BotState.FIGHTING)
        {
            foreach (BodyPart b in activeParts)
            {
                b.Deactivate();
            }
            state = BotState.VICTORIOUS;
        }
    }

    public void StartBattle()
    {
        state = BotState.FIGHTING;
    }
}