using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    public interface IDamageable
    {
        void TakeDamage(int dmg, Vector2 direction);
    }
}