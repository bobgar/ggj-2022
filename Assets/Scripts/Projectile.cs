using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 _direction;
        [SerializeField] private float moveSpeed;

        public void Setup(Vector3 direction)
        {
            this._direction = direction;
        }

        private void Update()
        {
            transform.position += _direction * moveSpeed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}