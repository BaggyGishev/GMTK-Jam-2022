using Gisha.GMTK2022.Core;
using UnityEngine;

namespace Gisha.GMTK2022.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health = 3;
        [SerializeField] private float attackDelay = 0.5f;
        [SerializeField] private float moveSpeed = 1f;
        protected float _delay;

        public float AttackDelay => attackDelay;

        public float MoveSpeed => moveSpeed;
        
        protected LayerMask _playerLayerMask;
        protected Transform _target;
        private Rigidbody2D _rb;

        public virtual void Awake()
        {
            _playerLayerMask = 1 << LayerMask.NameToLayer("Player");
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        public void TakeDamage(int dmg, Vector2 direction)
        {
            health -= dmg;

            if (health <= 0)
                Die();
            
            _rb.AddForce(direction * ResourceGetter.GameData.AttackImpulse, ForceMode2D.Impulse);
        }
    }
}