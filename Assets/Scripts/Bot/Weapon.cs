using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Bot
{
    public class Weapon : MonoBehaviour
    {
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

        protected virtual void FireEffect(BodyPart target, Vector3 position, Collider[] ignored)
        {
        }

        public virtual void Fire(BodyPart instigator, BotController enemyBot, Collider[] ignored)
        {
            if (enemyBot.activeParts.Length < 1)
            {
                Debug.Log("Enemy target " + enemyBot.name + " has no body parts!");
                return;
            }

            var liveParts = new List<BodyPart>();
            foreach (var part in enemyBot.activeParts)
                if (part.state != BodyPartState.DESTROYED)
                    liveParts.Add(part);

            if (liveParts.Count < 1)
            {
                Debug.Log("Attacking " + enemyBot.name + " with no live body parts!");
                return;
            }

            var target = liveParts[new Random().Next(0, liveParts.Count)];
            var distance = (transform.position - target.transform.position).magnitude;
            if (distance > range) return;

            if (audioSource) audioSource.Play();
            FireEffect(target, instigator.transform.position, ignored);

            Debug.Log(name + " attacks " + enemyBot.name + "'s " + target.name + " for " + damage);
            target.TakeDamage(damage);
        }
    }
}