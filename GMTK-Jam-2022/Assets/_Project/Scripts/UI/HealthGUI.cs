using Gisha.GMTK2022.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.GMTK2022.UI
{
    public class HealthGUI : MonoBehaviour
    {
        [SerializeField] private Image[] hearts;
        [SerializeField] private Sprite fullHeartSprite, emptyHeartSprite;

        private void OnEnable()
        {
            PlayerController.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            PlayerController.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int count)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < count)
                    hearts[i].sprite = fullHeartSprite;
                else
                    hearts[i].sprite = emptyHeartSprite;
            }
        }
    }
}