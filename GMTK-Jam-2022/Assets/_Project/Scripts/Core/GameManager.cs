using System;
using System.Collections;
using System.Collections.Generic;
using Gisha.GMTK2022.Player;
using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    public enum GameStage
    {
        None,
        Dicing,
        Battling
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private float delayBtwStages = 1.5f;
        [Space] [SerializeField] private float circularRadius = 2f;
        [SerializeField] private float maxRoundTime = 6f;

        public static Action RoundEnded;

        // Round Variables.
        private int _battleRounds;
        private int _enemyCount, _enemyType, _weaponType, _locationType;

        public float RoundTime { private set; get; }
        private GameData GameData => ResourceGetter.GameData;
        public float MaxRoundTime => maxRoundTime;
        public GameStage CurrentStage => _currentStage;
        public int BattleRounds => _battleRounds;

        private Stack<DiceResult> _diceResults = new Stack<DiceResult>();
        private EnemyGenerator _enemyGenerator;
        private LocationChanger _locationChanger;
        private PlayerController _playerController;

        private GameStage _currentStage;

        private void Awake()
        {
            Instance = this;

            _enemyGenerator = FindObjectOfType<EnemyGenerator>();
            _locationChanger = FindObjectOfType<LocationChanger>();
            _playerController = FindObjectOfType<PlayerController>();
        }

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
            if (stage.Equals(CurrentStage))
                Debug.LogError("Stage overlapping.");
            _currentStage = stage;

            switch (stage)
            {
                // Instantiate dices, create round rules. Watch the dices.
                case GameStage.Dicing:
                    StartCoroutine(DiceStageRoutine());
                    break;
                // Initialize round rules. Watch enemies count.
                case GameStage.Battling:
                    StartCoroutine(BattleStageRoutine());
                    break;
            }
        }

        #region Stage Routines

        private IEnumerator BattleStageRoutine()
        {
            _playerController.TakeWeapon(ResourceGetter.GameData.WeaponPrefabs[_weaponType]);
            _locationChanger.SetupBattleLocation(_locationType);

            // Spawning enemies.
            while (BattleRounds > 0)
            {
                _battleRounds = BattleRounds - 1;
                _enemyGenerator.Generate(_enemyType, _enemyCount);
                yield return RoundRoutine();
            }

            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                yield return new WaitForSeconds(0.25f);

            InitiateStage(GameStage.Dicing);
        }

        private IEnumerator RoundRoutine()
        {
            RoundTime = MaxRoundTime;
            RoundEnded?.Invoke();

            while (RoundTime > 0f)
            {
                RoundTime -= Time.deltaTime;
                yield return null;
                RoundEnded?.Invoke();
            }
        }


        private IEnumerator DiceStageRoutine()
        {
            yield return null;
            _playerController.ResetWeapon();
            _locationChanger.SetupBettingLocation();

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
        }

        #endregion


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
            rulesChanger.WeaponSetup(out _weaponType);
            rulesChanger.LocationSetup(out _locationType);
            yield return new WaitForSeconds(delayBtwStages);
            rulesChanger.EnemySetup(out _enemyType, out _enemyCount);
            rulesChanger.MasterSetup(out _battleRounds);

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

        public void MasterSetup(out int roundsCount)
        {
            roundsCount = _masterRule.Result;
        }

        public void WeaponSetup(out int weaponType)
        {
            weaponType = _weaponRule.Result - 1;
        }

        public void LocationSetup(out int locationType)
        {
            locationType = _locationRule.Result - 1;
        }

        public void EnemySetup(out int enemyType, out int enemyCount)
        {
            enemyType = _enemyTypeRule.Result - 1;
            enemyCount = _enemyCountRule.Result;
        }
    }
}