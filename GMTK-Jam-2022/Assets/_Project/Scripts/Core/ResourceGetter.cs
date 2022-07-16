using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    public static class ResourceGetter
    {
        public static GameData GameData => (GameData) Resources.Load("GameData", typeof(GameData));
    }
}