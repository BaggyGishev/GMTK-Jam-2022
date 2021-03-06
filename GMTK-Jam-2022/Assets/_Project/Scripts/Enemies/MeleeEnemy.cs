using Gisha.GMTK2022.Core;
using UnityEngine;

namespace Gisha.GMTK2022.Enemies
{
    public class MeleeEnemy : Enemy
    {
        [Header("Melee Variables")] public float attackDst;
        private Vector2 _viewDir;

        private void FixedUpdate()
        {
            _viewDir = (_target.transform.position - transform.position).normalized;
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, _viewDir, attackDst, _playerLayerMask);

            // If nothing is in front of an enemy.
            if (hitInfo.collider == null)
            {
                FollowPlayer();
                _delay = AttackDelay;

                _animator.SetBool("IsIdle", false);
            }
            else
            {
                if (_delay <= 0)
                    DamagePlayer();
                else
                    _delay -= Time.deltaTime;

                _animator.SetBool("IsIdle", true);
            }
        }

        private void DamagePlayer()
        {
            _target.GetComponent<IDamageable>().TakeDamage(1, _viewDir);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.right * attackDst);
        }
    }
}