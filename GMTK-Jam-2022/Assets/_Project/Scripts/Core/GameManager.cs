using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float circularRadius = 2f;
        
        private GameData GameData => ResourceGetter.GameData;

        private enum GameStage
        {
            None,
            Dicing,
            Battling
        }

        private GameStage _currentStage;

        private void Start()
        {
            InitiateStage(GameStage.Dicing);
        }

        private void InitiateStage(GameStage stage)
        {
            if (stage.Equals(_currentStage))
                Debug.LogError("Stage overlapping.");
            _currentStage = stage;

            switch (stage)
            {
                // Instantiate dices, create round rules. Watch the dices.
                case GameStage.Dicing:
                    Instantiate(GameData.MasterDicePrefab, Vector3.zero, Quaternion.identity);

                    for (int i = 0; i < GameData.RulesDicePrefabs.Length; i++)
                    {
                        var rad = 2 * Mathf.PI / GameData.RulesDicePrefabs.Length * i;
                        var h = Mathf.Cos(rad);
                        var v = Mathf.Sin(rad);
                        var position = Vector3.zero + new Vector3(h, v, 0f) * circularRadius;

                        Instantiate(GameData.RulesDicePrefabs[i], position, Quaternion.identity);
                    }

                    break;
                // Initialize round rules. Watch enemies count.
                case GameStage.Battling:
                    break;
            }
        }
    }
}