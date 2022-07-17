using Gisha.GMTK2022.Core;
using UnityEngine;

namespace Gisha.GMTK2022.UI
{
    public class RoundTimerGUI : MonoBehaviour
    {
        private void Update()
        {
            if (GameManager.Instance.CurrentStage != GameStage.Battling)
            {
                transform.localScale = Vector3.zero;
                return;
            }
            
            float size = GameManager.Instance.RoundTime / GameManager.Instance.MaxRoundTime;
            transform.localScale = new Vector3(size, 1f, 1f);
        }
    }
}