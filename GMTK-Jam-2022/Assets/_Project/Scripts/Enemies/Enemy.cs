using Gisha.GMTK2022.Core;
using UnityEngine;

namespace Gisha.GMTK2022.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health;
        [SerializeField] private float attackDelay;
        [SerializeField] private float moveSpeed;
        
        protected float _delay;

        public float AttackDelay => attackDelay;

        public float MoveSpeed => moveSpeed;

        private Rigidbody2D _rb;

        private void Awake()
        {
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