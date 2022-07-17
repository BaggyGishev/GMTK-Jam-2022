using System.Collections;
using System.Collections.Generic;
using Gisha.GMTK2022.Player;
using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float delayBtwStages = 1.5f;
        [Space] [SerializeField] private float circularRadius = 2f;

        private GameData GameData => ResourceGetter.GameData;

        private Stack<DiceResult> _diceResults = new Stack<DiceResult>();

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

        private void OnEnable()
        {
            Dice.DiceRolled += OnDiceRolled;
        }

        private void OnDisable()
        {
            Dice.DiceRolled -= OnDiceRolled;
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
                    _diceResults.Clear();

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

        private void OnDiceRolled(DiceResult diceResult)
        {
            _diceResults.Push(diceResult);

            var dices = FindObjectsOfType<Dice>();
            if (dices == null || dices.Length == 0)
                StartCoroutine(SetupRulesRoutine());
        }

        private IEnumerator SetupRulesRoutine()
        {
            var rulesChanger = new RulesChanger(_diceResults);
            rulesChanger.MasterSetup();
            rulesChanger.WeaponSetup();
            rulesChanger.LocationSetup();
            yield return new WaitForSeconds(delayBtwStages);
            rulesChanger.EnemySetup();
            InitiateStage(GameStage.Battling);
        }
    }

    public class RulesChanger
    {
        private DiceResult _masterRule;
        private DiceResult _weaponRule;
        private DiceResult _locationRule;
        private DiceResult _enemyCountRule;
        private DiceResult _enemyTypeRule;

        public RulesChanger(Stack<DiceResult> results)
        {
            while (results.Count > 0)
            {
                var result = results.Pop();
                Debug.Log(results.Count);
                switch (result.DiceType)
                {
                    case DiceType.Master:
                        _masterRule = result;
                        break;
                    case DiceType.Location:
                        _locationRule = result;
                        break;
                    case DiceType.EnemyCount:
                        _enemyCountRule = result;
                        break;
                    case DiceType.EnemyType:
                        _enemyTypeRule = result;
                        break;
                    case DiceType.WeaponType:
                        _weaponRule = result;
                        break;
                }
            }
        }

        public void MasterSetup()
        {
        }

        public void WeaponSetup()
        {
            var prefab = ResourceGetter.GameData.WeaponPrefabs[0];
            Object.FindObjectOfType<PlayerController>().TakeWeapon(prefab);
        }

        public void LocationSetup()
        {
            Object.FindObjectOfType<LocationChanger>().SetupBattleLocation(_locationRule.Result);
        }

        public void EnemySetup()
        {
            Object.FindObjectOfType<EnemyGenerator>().Generate(0, 2);
        }
    }
}