using UnityEngine;

namespace Gisha.GMTK2022.Enemies
{
    public class RangeEnemy : Enemy
    {
        [Header("Ranged Variables")] [SerializeField]
        private float shootRayDist;

        [SerializeField] private GameObject projectilePrefab;

        private Vector2 _viewDir;

        private void Update()
        {
            _viewDir = (_target.transform.position - transform.position).normalized;
            RaycastHit2D shootHitInfo = Physics2D.Raycast(transform.position, _viewDir, shootRayDist, _playerLayerMask);

            if (shootHitInfo.collider == null)
            {
                FollowPlayer();
                _animator.SetBool("IsIdle", false);
            }

            else
            {
                if (_delay <= 0f)
                {
                    ShootProjectile();
                    _delay = AttackDelay;
                }
                else
                    _delay -= Time.deltaTime;
                
                _animator.SetBool("IsIdle", true);
            }
        }
        
        private void ShootProjectile()
        {
            float rotZ = Mathf.Atan2(_viewDir.y, _viewDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ);

            Instantiate(projectilePrefab, transform.position, rotation);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.right * shootRayDist);
        }
    }
}