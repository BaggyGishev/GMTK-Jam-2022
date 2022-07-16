using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private float moveSpeed = 1f;

        private int _health;
        private Vector2 _moveInput;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _health = maxHealth;
        }

        private void Update()
        {
            _moveInput = GetKeyboardInput();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void TakeDamage()
        {
            _health--;

            if (_health <= 0)
                Die();
        }

        private Vector2 GetKeyboardInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        private void Move()
        {
            _rb.velocity = _moveInput * moveSpeed * Time.deltaTime;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}