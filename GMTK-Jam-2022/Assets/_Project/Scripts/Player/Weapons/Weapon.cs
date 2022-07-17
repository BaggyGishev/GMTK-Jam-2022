using System.Collections;
using UnityEngine;

namespace Gisha.GMTK2022.Player.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float attackDelay;
        
        protected bool IsDelayed { private set; get; }

        public abstract void Use();

        protected IEnumerator DelayRoutine()
        {
            IsDelayed = true;
            yield return new WaitForSeconds(attackDelay);
            IsDelayed = false;
        }
    }
}