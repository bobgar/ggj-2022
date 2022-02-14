using UnityEngine;

namespace Bot
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private Vector3 _direction;

        private void Update()
        {
            transform.position += _direction * moveSpeed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }

        public void Setup(Vector3 direction)
        {
            _direction = direction;
        }
    }
}