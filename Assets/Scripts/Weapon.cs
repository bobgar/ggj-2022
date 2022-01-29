using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;


public class Weapon : MonoBehaviour
{
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

    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] public float attackRate;

    public void Fire(BotController enemyBot)
    {
        if (enemyBot.bodyParts.Length < 1)
        {
            Debug.Log("Enemy target " + enemyBot.name + " has no body parts!");
            return;
        }

        BodyPart target = enemyBot.bodyParts[new Random().Next(0, enemyBot.bodyParts.Length)];
        float distance = (transform.position - target.transform.position).magnitude;
        if (distance > range) return;

        target.TakeDamage(damage);
    }
}