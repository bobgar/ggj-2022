using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private Transform projectile;

        protected override void FireEffect(BodyPart target, Vector3 position)
        {
            Transform projectileTransform = Instantiate(projectile, position, Quaternion.identity);
            Vector3 direction = (target.transform.position - position).normalized;
            projectileTransform.GetComponent<Projectile>().Setup(direction);
        }
    }
}