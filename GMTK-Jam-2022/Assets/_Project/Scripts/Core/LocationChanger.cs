using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    public class LocationChanger : MonoBehaviour
    {
        [SerializeField] private GameObject bettingLocation;
        [SerializeField] private GameObject[] battleLocations;

        private GameObject _currentLevel;

        private void Start()
        {
            _currentLevel = bettingLocation;
        }

        public void SetupBettingLocation()
        {
            _currentLevel.SetActive(false);
            _currentLevel = bettingLocation;
            _currentLevel.SetActive(true);
        }

        public void SetupBattleLocation(int index)
        {
            _currentLevel.SetActive(false);
            _currentLevel = battleLocations[index];
            _currentLevel.SetActive(true);
        }
    }
}