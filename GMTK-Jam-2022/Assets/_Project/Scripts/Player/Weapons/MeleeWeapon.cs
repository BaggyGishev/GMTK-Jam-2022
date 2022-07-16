using Gisha.GMTK2022.Core;
using UnityEngine;

namespace Gisha.GMTK2022.Player.Weapons
{
    public class MeleeWeapon : Weapon
    {
        [Header("Melee Variables")] [SerializeField]
        private int attackDmg = 1;

        [SerializeField] private float attackDst = 1f;
        [SerializeField] private float attackRadius = 0.15f;

        private LayerMask _enemyLayerMask;
        Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        }

        public override void Use()
        {
            _animator.SetTrigger("Use");

            var hits =
                Physics2D.CircleCastAll(transform.position, attackRadius, GetDirectionToMouse(), attackDst,
                    _enemyLayerMask);

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                    hit.collider.GetComponent<IDamageable>().TakeDamage(attackDmg);
            }
        }

        private Vector2 GetDirectionToMouse()
        {
            return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Ray ray;

            if (Application.isPlaying)
                ray = new Ray(transform.position, GetDirectionToMouse());
            else
                ray = new Ray(transform.position, Vector3.right);

            Gizmos.DrawRay(ray.origin, ray.direction * attackDst);
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}