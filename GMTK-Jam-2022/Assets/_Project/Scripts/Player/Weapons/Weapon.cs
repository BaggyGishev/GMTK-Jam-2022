using System.Collections;
using Gisha.Effects.Audio;
using UnityEngine;

namespace Gisha.GMTK2022.Player.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float attackDelay;
        [SerializeField] private string attackSFX;

        protected bool IsDelayed { private set; get; }

        public string AttackSfx => attackSFX;

        public abstract void Use();

        protected IEnumerator DelayRoutine()
        {
            IsDelayed = true;
            yield return new WaitForSeconds(attackDelay);
            IsDelayed = false;
        }
    }
}