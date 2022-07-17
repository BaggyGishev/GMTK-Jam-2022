using Gisha.GMTK2022.Core;
using TMPro;
using UnityEngine;

namespace Gisha.GMTK2022.UI
{
    public class WinStreakGUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text winStreakText;

        private void OnEnable()
        {
            GameManager.LevelWon += OnLevelWon;
        }

        private void OnDisable()
        {
            GameManager.LevelWon -= OnLevelWon;
        }

        private void OnLevelWon(int streak)
        {
            winStreakText.text = streak.ToString();
        }
    }
}