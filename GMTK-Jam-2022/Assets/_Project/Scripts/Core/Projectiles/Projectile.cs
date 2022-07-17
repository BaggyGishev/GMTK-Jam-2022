using Gisha.Effects.Audio;
using UnityEngine;

namespace Gisha.GMTK2022.Core.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Variables")] [SerializeField]
        private float lifeTime = 5f;

        [SerializeField] private float flySpeed = 3f;
        [SerializeField] private int damage;

        [SerializeField] private Transform pivotPoint;
        [SerializeField] private LayerMask whatIsSolid;

        [SerializeField] private string destroySFXName;
        
        
        private Vector2 _direction;

        private void Start()
        {
            Invoke("DestroyProjectile", lifeTime);

            _direction = transform.InverseTransformDirection(pivotPoint.up);
        }

        private void Update()
        {
            transform.Translate(_direction * flySpeed * Time.deltaTime);

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, _direction, 0.25f, whatIsSolid);
            if (hitInfo.collider != null)
                OnCollide(hitInfo.collider);
        }

        public virtual void OnCollide(Collider2D hitCollider)
        {
            DestroyProjectile();

            if (hitCollider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(damage, pivotPoint.up);
        }

        private void DestroyProjectile()
        {
            Destroy(gameObject);
            AudioManager.Instance.PlaySFX(destroySFXName);
        }
    }
}