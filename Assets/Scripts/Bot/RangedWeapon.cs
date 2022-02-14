using UnityEngine;

namespace Bot
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private Transform projectile;

        protected override void FireEffect(BodyPart target, Vector3 position, Collider[] ignored)
        {
            // Hack to line up with cannon nozzle
            position = new Vector3(position.x, position.y + 6.0f, position.z);
            var projectileTransform = Instantiate(projectile, position, Quaternion.identity);
            foreach (var collider in ignored)
                Physics.IgnoreCollision(projectileTransform.GetComponent<Collider>(), collider);

            var direction = (target.transform.position - position).normalized;
            projectileTransform.GetComponent<Projectile>().Setup(direction);
        }
    }
}