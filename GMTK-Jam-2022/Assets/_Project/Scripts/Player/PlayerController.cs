using System;
using System.Collections;
using Gisha.GMTK2022.Core;
using Gisha.GMTK2022.Player.Weapons;
using UnityEngine;

namespace Gisha.GMTK2022.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private Transform handTrans;
        [Space] [SerializeField] private float invincibleTime = 0.2f;

        public static Action<int> HealthChanged;
        public static Action Died;

        private float _stunDelay;
        private int _health;
        private bool _isInvincible;

        private Vector2 _moveInput;
        private Rigidbody2D _rb;
        private Weapon _weapon;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _health = maxHealth;
        }

        private void Update()
        {
            _moveInput = GetKeyboardInput();

            if (Input.GetMouseButtonDown(0) && _weapon != null)
                _weapon.Use();

            handTrans.rotation = GetHandRotation();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void TakeWeapon(GameObject weaponPrefab)
        {
            _weapon = Instantiate(weaponPrefab, handTrans).GetComponent<Weapon>();
            _weapon.transform.position = handTrans.position;
        }

        public void ResetWeapon()
        {
            if (_weapon == null)
                return;

            Destroy(_weapon.gameObject);
            _weapon = null;
        }

        public void TakeDamage(int dmg, Vector2 direction)
        {
            if (_isInvincible)
                return;

            _stunDelay = 0.2f;
            _rb.AddForce(direction.normalized * ResourceGetter.GameData.AttackImpulse, ForceMode2D.Impulse);

            _health -= dmg;
            if (_health < 0)
                Die();

            Debug.Log(_health);
            HealthChanged(_health);
            _animator.SetTrigger("TakeDamage");

            // Little helper for the player to reduce instant kill scenario from various targets.
            StartCoroutine(InvincibleRoutine());
        }

        public void HealOne()
        {
            _health++;
            if (_health > maxHealth)
                _health = maxHealth;

            HealthChanged(_health);
        }


        private Vector2 GetKeyboardInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        public Quaternion GetHandRotation()
        {
            Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, rotZ);
        }

        private void Move()
        {
            if (_stunDelay > 0f)
            {
                _stunDelay -= Time.deltaTime;
                _animator.SetBool("IsIdle", true);
                return;
            }

            _animator.SetBool("IsIdle", _moveInput.magnitude <= 0);
            _rb.velocity = _moveInput * moveSpeed * Time.deltaTime;
        }

        private void Die()
        {
            Died?.Invoke();
            gameObject.SetActive(false);
        }

        private IEnumerator InvincibleRoutine()
        {
            _isInvincible = true;
            yield return new WaitForSeconds(invincibleTime);
            _isInvincible = false;
        }
    }
}