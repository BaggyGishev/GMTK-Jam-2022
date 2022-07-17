using Gisha.GMTK2022.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.GMTK2022.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject losePopup;

        private void OnEnable()
        {
            PlayerController.Died += OnPlayerDied;
        }


        private void OnDisable()
        {
            PlayerController.Died -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            losePopup.SetActive(true);
        }

        public void OnClick_ReturnToMenu()
        {
        }

        public void OnClick_Restart()
        {
            SceneManager.LoadScene("Game");
        }
    }
}