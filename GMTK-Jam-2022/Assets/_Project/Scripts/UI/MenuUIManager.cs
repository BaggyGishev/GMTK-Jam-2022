using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.GMTK2022
{
    public class MenuUIManager : MonoBehaviour
    {
        public void OnClick_Play()
        {
            SceneManager.LoadScene("Game");
        }
        
        public void OnClick_Settings()
        {
            
        }
        
        public void OnClick_Quit()
        {
            Application.Quit();
        }
    }
}
