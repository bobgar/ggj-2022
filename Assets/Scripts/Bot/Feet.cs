using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bot
{
    public class Feet : BodyPart
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float playThreshold = 0.01f;

        public float accelerationForward = .0005f;
        public float accelerationReverse = .00025f;
        public float jumpChance = .0001f;

        public float _lastMovement;
        private readonly float _dampening = .985f;

        private Vector3 _velocity;

        public void Start()
        {
            base.Start();
            if (audioSource)
            {
                audioSource.loop = true;
                audioSource.volume = 0.1f;
            }
        }

        private void FixedUpdate()
        {
            if (botController.State == BotState.FIGHTING) Move();
        }

        private void LateUpdate()
        {
            if (audioSource && _lastMovement <= playThreshold) audioSource.Pause();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            if (audioSource != null) audioSource.Stop();
        }

        private void Move()
        {
            var distance = botController.target.transform.position - botController.transform.position;

            var acceleration = Vector3.Normalize(distance);
            acceleration.y = 0;

            if (distance.magnitude > botController.GetDesiredMaxDistance())
            {
                acceleration *= accelerationForward;
                _velocity += acceleration;
            }
            else if (distance.magnitude < botController.GetDesiredMinDistance())
            {
                acceleration *= -accelerationReverse;
                _velocity += acceleration;
            }

            if (Random.value < jumpChance) _velocity += new Vector3(0f, .05f, 0f);

            var previousPosition = botController.transform.position;
            botController.transform.position += _velocity;

            _lastMovement = Math.Abs((previousPosition - botController.transform.position).magnitude);

            _velocity *= _dampening;
            if (audioSource && !audioSource.isPlaying && _lastMovement > playThreshold) audioSource.Play();
        }
    }
}