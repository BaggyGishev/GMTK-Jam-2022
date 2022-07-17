using Gisha.Effects.Audio;
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
            AudioManager.Instance.PlaySFX("lose");
        }

        public void OnClick_ReturnToMenu()
        {
            SceneManager.LoadScene("Menu");
            AudioManager.Instance.PlaySFX("click");
        }

        public void OnClick_Restart()
        {
            SceneManager.LoadScene("Game");
            AudioManager.Instance.PlaySFX("click");
        }
    }
}