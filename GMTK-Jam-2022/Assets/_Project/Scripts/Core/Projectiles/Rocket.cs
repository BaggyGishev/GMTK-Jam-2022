using UnityEngine;

namespace Gisha.GMTK2022.Core.Projectiles
{
    public class Rocket : Projectile
    {
        [Header("Rocket Variables")] [SerializeField]
        private GameObject subProjectilePrefab;

        [SerializeField] private int projCount;

        public override void OnCollide(Collider2D hitCollider)
        {
            float deltaZ = (2 * Mathf.PI / projCount) * Mathf.Rad2Deg;
            for (int i = 0; i < projCount; i++)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, deltaZ * i);
                Instantiate(subProjectilePrefab, transform.position, rotation);
            }

            base.OnCollide(hitCollider);
        }
    }
}