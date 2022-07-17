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

        private float _stunDelay;
        private int _health;
        private Vector2 _moveInput;
        private Rigidbody2D _rb;
        private Weapon _weapon;

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

        public void TakeDamage(int dmg, Vector2 direction)
        {
            StartCoroutine(DamageGettingRoutine(dmg, direction));
        }

        private IEnumerator DamageGettingRoutine(int dmg, Vector2 direction)
        {
            _stunDelay = 0.2f;
            _rb.AddForce(direction.normalized * ResourceGetter.GameData.AttackImpulse, ForceMode2D.Impulse);

            yield return new WaitForEndOfFrame();
            _health -= dmg;
            if (_health < 0)
                Die();
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
                return;
            }

            _rb.velocity = _moveInput * moveSpeed * Time.deltaTime;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}