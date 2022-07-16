using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private GameObject masterDicePrefab;
        [SerializeField] private GameObject[] rulesDicePrefabs;
        
        [SerializeField] private GameObject[] weaponPrefabs;
        [SerializeField] private GameObject[] enemyPrefabs;

        [SerializeField] private float attackImpulse = 1f;
                

        public GameObject[] RulesDicePrefabs => rulesDicePrefabs;
        public GameObject MasterDicePrefab => masterDicePrefab;
        public GameObject[] WeaponPrefabs => weaponPrefabs;
        public GameObject[] EnemyPrefabs => enemyPrefabs;

        public float AttackImpulse => attackImpulse;
    }
}