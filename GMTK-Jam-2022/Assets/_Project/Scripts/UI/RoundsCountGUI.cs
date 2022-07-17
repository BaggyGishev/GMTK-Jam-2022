using System;
using Gisha.GMTK2022.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.GMTK2022.UI
{
    public class RoundsCountGUI : MonoBehaviour
    {
        [SerializeField] private Sprite[] roundCountSprites;
        [SerializeField] private Image roundCountImage;

        private void Start()
        {
            roundCountImage.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            GameManager.RoundEnded += OnRoundEnded;
        }

        private void OnDisable()
        {
            GameManager.RoundEnded -= OnRoundEnded;
        }

        private void OnRoundEnded()
        {
            if (GameManager.Instance.BattleRounds == 0)
            {
                roundCountImage.gameObject.SetActive(false);
                return;
            }

            roundCountImage.gameObject.SetActive(true);
            roundCountImage.sprite = roundCountSprites[GameManager.Instance.BattleRounds - 1];
        }
    }
}