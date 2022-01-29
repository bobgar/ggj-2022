using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum attackType
{
    MELEE,
    RANGED_SINGLE_POINT,
    RANGED_AOE,
    RANGED_PROJECTILE,
}

enum damageType
{
    PIERCE,
    SLASH,
    BLUNT,
    FIRE
}

public class Arm : BodyPart
{
    //Characteristics of Arms.  What do arms have?

    private int _hitpoints;
    

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
