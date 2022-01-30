using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public float attackRate;

    public Weapon()
    {
        if (audioSource)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    public void Fire(BotController enemyBot)
    {
        if (audioSource)
        {
            audioSource.Play();
        }

        if (enemyBot.activeParts.Length < 1)
        {
            Debug.Log("Enemy target " + enemyBot.name + " has no body parts!");
            return;
        }

        List<BodyPart> liveParts = new List<BodyPart>();
        foreach (BodyPart part in enemyBot.activeParts)
        {
            if (part.state != BodyPartState.DESTROYED)
            {
                liveParts.Add(part);
            }
        }

        if (liveParts.Count < 1)
        {
            Debug.Log("Attacking " + enemyBot.name + " with no live body parts!");
            return;
        }

        BodyPart target = liveParts[new Random().Next(0, liveParts.Count)];
        float distance = (transform.position - target.transform.position).magnitude;
        if (distance > range) return;

        Debug.Log(this.name + " attacks " + enemyBot.name + "'s " + target.name + " for " + damage);
        target.TakeDamage(damage);
    }
}