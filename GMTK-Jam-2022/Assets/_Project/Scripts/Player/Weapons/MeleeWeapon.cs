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
                Physics2D.CircleCastAll(transform.position, attackRadius, transform.right, attackDst,
                    _enemyLayerMask);

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                    hit.collider.GetComponent<IDamageable>().TakeDamage(attackDmg);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawRay(transform.position, transform.right * attackDst);
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}