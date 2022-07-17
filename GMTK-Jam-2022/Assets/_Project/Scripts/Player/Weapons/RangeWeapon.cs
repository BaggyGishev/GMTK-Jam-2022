using Gisha.Effects.Audio;
using UnityEngine;

namespace Gisha.GMTK2022.Player.Weapons
{
    public class RangeWeapon : Weapon
    {
        [Header("Range Variables")] [SerializeField]
        private Transform shotPos;

        [SerializeField] private GameObject projectilePrefab;

        [SerializeField] private int projCount;
        [SerializeField] private float spread;

        Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Use()
        {
            if (IsDelayed)
                return;

            AudioManager.Instance.PlaySFX(AttackSfx);

            _animator.SetTrigger("Use");
            Shoot();
            StartCoroutine(DelayRoutine());
        }

        private void Shoot()
        {
            float initRotZ = Mathf.Atan2(-shotPos.up.y, -shotPos.up.x) * Mathf.Rad2Deg;

            if (projCount == 1)
                spread = 0;

            for (int i = 0; i < projCount; i++)
            {
                float rotZ = initRotZ + Random.Range(-spread, spread);
                Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ);
                Instantiate(projectilePrefab, shotPos.position, rotation);
            }
        }
    }
}