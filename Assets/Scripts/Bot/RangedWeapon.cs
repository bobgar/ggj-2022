﻿using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private Transform projectile;

        protected override void FireEffect(BodyPart target, Vector3 position, Collider[] ignored)
        {
            // Hack to line up with cannon nozzle
            position = new Vector3(position.x, position.y + 6.0f, position.z);
            Transform projectileTransform = Instantiate(projectile, position, Quaternion.identity);
            foreach (Collider collider in ignored)
            {
                Physics.IgnoreCollision(projectileTransform.GetComponent<Collider>(), collider);
            }

            Vector3 direction = (target.transform.position - position).normalized;
            projectileTransform.GetComponent<Projectile>().Setup(direction);
        }
    }
}