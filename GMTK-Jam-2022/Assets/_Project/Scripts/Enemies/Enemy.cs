using System;
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

        public static Action EnemyDestroyed;

        public float AttackDelay => attackDelay;

        public float MoveSpeed => moveSpeed;

        protected LayerMask _playerLayerMask;
        protected Transform _target;
        protected Animator _animator;
        private Rigidbody2D _rb;

        public virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerLayerMask = 1 << LayerMask.NameToLayer("Player");
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Die()
        {
            EnemyDestroyed?.Invoke();
            Destroy(gameObject);
        }

        protected void FollowPlayer()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, _target.transform.position, MoveSpeed * Time.deltaTime);
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