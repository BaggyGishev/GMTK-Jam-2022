using System.Collections;
using System.Collections.Generic;
using Gisha.Effects.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.GMTK2022
{
    public class MenuUIManager : MonoBehaviour
    {
        public void OnClick_Play()
        {
            SceneManager.LoadScene("Game");
            AudioManager.Instance.PlaySFX("click");
        }
        
        public void OnClick_Settings()
        {
            AudioManager.Instance.PlaySFX("click");
        }
        
        public void OnClick_Quit()
        {
            Application.Quit();
            AudioManager.Instance.PlaySFX("click");
        }
    }
}
